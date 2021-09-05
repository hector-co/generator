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
    domainSettings: {
      aggregateRootBaseClass: 'Agg<:T0:>',
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
                var a = new ModelTemplate(module, model.Value);
                var t = a.TransformText();
                Console.WriteLine(t);
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
