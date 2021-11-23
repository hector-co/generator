using Generator.Metadata;
using Generator.Templates.Domain;
using Newtonsoft.Json;
using System;

namespace Generator.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var str = @"
  {
    name: 'Module1',
    'domainSettings': {
      aggregateRootBaseClass: 'AggregateRoot<:T0:>',
      entityBaseClass: 'Entity<:T0:>',
      entityUsings: [ 'Hco.Base.Domain' ],
      generateIdProperties: false
    },
    'models': {
      'Model1': {
        'properties': {
          'Prop1': {
            'typeName': 'string'
          },
          'Prop3': {
            'typeName': 'List<string>'
          }
        },
        'isRoot': false
      },
      'Model2': {
        'properties': {
          'Prop1': {
            'typeName': 'string'
          },
          SubModel2: {
            typeName: 'List<Model2>'
          }, 
          'Model1Rel': {
            'typeName': 'Model1'
          },
          'Model1Rel2': {
            'typeName': 'List<Model1>'
          },
          'Address': {
            'typeName': 'Address'
          },
          'Addresses': {
            'typeName': 'List<Address>'
          }
        }
      },
      'Model3': {
        'properties': {
          'Name': {
            'typeName': 'string'
          },
          'Model2Rel': {
            'typeName': 'Model2'
          }
        }
      },
      'Model4': {
        'properties': {
          'Name': {
            'typeName': 'string'
          },
          'Model3Rel': {
            'typeName': 'List<Model3>'
          },
          'Model2Rel': {
            'typeName': 'Model2?'
          }
        }
      },
      'Model5': {
        'properties': {
          'Name': {
            'typeName': 'string'
          },
          'Model3Rel': {
            'typeName': 'List<Model3>',
            withMany: true
          }
        }
      },
      'Model6': {
        'properties': {
          'Name': {
            'typeName': 'string'
          }
        }
      },
      'Model7': {
        'properties': {
          'Model6Rel': {
            'typeName': 'List<Model6>'
          }
        }
      },
      'Address': {
        'properties': {
          'Prop1': {
            'typeName': 'string'
          }
        },
        'isEntity': false
      }
    }
  }";

            var module = JsonConvert.DeserializeObject<ModuleDefinition>(str);
            module.Init();

            foreach (var model in module.Models)
            {
                var modelTpl = new ModelTemplate(module, model.Value);
                var text = modelTpl.TransformText();
                System.IO.File.WriteAllText($"E:/temp/gentest/{model.Key}.cs", text);
                Console.WriteLine(text);
                Console.WriteLine();
            }

            var dataAccesTpl = new DataAccessTemplate(module);
            var text2 = dataAccesTpl.TransformText();
            System.IO.File.WriteAllText($"E:/temp/gentest/_DataAccess.Ef.cs", text2);
            Console.WriteLine(text2);
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
