using Generator.Metadata;
using Generator.Templates.Domain;
using Generator.Templates.Queries;
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
          },
          'Value1': {
            'typeName': 'int?'
          }
        }
      },
      'Model3': {
        'properties': {
          'Name': {
            'typeName': 'string'
          },
          'Model2Rel': {
            'typeName': 'Model2',
            'filter': {
              'apply': false
            }
          }
        },
        'identifierType': 'Guid'
      },
      'Model4': {
        'properties': {
          'Name': {
            'typeName': 'string'
          },
          'Model3Rel': {
            'typeName': 'List<Model3>',
            'filter': {
              'apply': true,
              'type': 'equals'
            }
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
            'withMany': true
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
            'typeName': 'List<Model6>',
            'filter': {
              'apply': true,
              'type': 'range'
            }
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
                System.IO.File.WriteAllText($"E:/temp/gentest2/Domain/Model/{model.Key}.cs", text);
                Console.WriteLine(text);
                Console.WriteLine();

                var dtoTpl = new DtoTemplate(module.Name, model.Value);
                var dtoText = dtoTpl.TransformText();
                System.IO.File.WriteAllText($"E:/temp/gentest2/Queries/{model.Key}Dto.cs", dtoText);
                Console.WriteLine(dtoText);
                Console.WriteLine();

                var getByIdTpl = new GetByIdQueryTemplate(module.Name, model.Value);
                var getByIdText = getByIdTpl.TransformText();
                System.IO.File.WriteAllText($"E:/temp/gentest2/Queries/{model.Key}DtoGetById.cs", getByIdText);
                Console.WriteLine(getByIdText);
                Console.WriteLine();

                var pagedTpl = new PagedQueryTemplate(module.Name, model.Value);
                var pagedText = pagedTpl.TransformText();
                System.IO.File.WriteAllText($"E:/temp/gentest2/Queries/{model.Key}DtoPagedQuery.cs", pagedText);
                Console.WriteLine(pagedText);
                Console.WriteLine();
            }

            var dataAccesTpl = new DataAccessTemplate(module);
            var text2 = dataAccesTpl.TransformText();
            System.IO.File.WriteAllText($"E:/temp/gentest2/Domain/Model/_DataAccess.Ef.cs", text2);
            Console.WriteLine(text2);
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
