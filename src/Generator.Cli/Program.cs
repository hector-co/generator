﻿using Generator.Metadata;
using Generator.Templates.DataAccessEf;
using Generator.Templates.Domain;
using Generator.Templates.Queries;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Generator.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var str = @"
  {
    name: 'Module1',
    'databaseSchema': 'public',
    'domainSettings': {
      entityBaseClass: 'Entity<:T0:>',
      entityUsings: [ 'Hco.Base.Domain' ],
      generateIdProperties: false
    },
    'models': {
      'Model1': {
        'properties': {
          'Prop1': {
            'typeName': 'string',
            'size': 20
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
          'Model1Rel3': {
            'typeName': 'Model1'
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
          'Model3Rel2': {
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
            'typeName': 'string',
            'required': true
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

                if (model.Value.IsEntity)
                {
                    if (!model.Value.IsOwnedEntity)
                    {
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

                        var getByIdHandlerTpl = new GetByIdQueryHandlerTemplate(module.Name, model.Value);
                        var getByIdHandlerText = getByIdHandlerTpl.TransformText();
                        System.IO.File.WriteAllText($"E:/temp/gentest2/DataAccess/Queries/{model.Key}DtoGetByIdQueryHandler.cs", getByIdHandlerText);
                        Console.WriteLine(getByIdHandlerText);
                        Console.WriteLine();

                        var pagedHandlerTpl = new PagedQueryHandlerTemplate(module.Name, model.Value);
                        var pagedHandlerText = pagedHandlerTpl.TransformText();
                        System.IO.File.WriteAllText($"E:/temp/gentest2/DataAccess/Queries/{model.Key}DtoPagedQueryHandler.cs", pagedHandlerText);
                        Console.WriteLine(pagedHandlerText);
                        Console.WriteLine();

                        foreach (var property in model.Value.Properties.Values.Where(p => p.RelationRequiresJoinModel()))
                        {
                            var joinConfigTpl = new JoinModelConfigurationTemplate(module.Name, model.Value, property);
                            var joinConfigText = joinConfigTpl.TransformText();
                            System.IO.File.WriteAllText($"E:/temp/gentest2/DataAccess/{model.Value.GetJoinModelTypeName(property)}.cs", joinConfigText);
                            Console.WriteLine(joinConfigText);
                            Console.WriteLine();
                        }

                        if (QueryableExtensionsTemplate.RequiresQueryableExtensions(model.Value))
                        {
                            var extensionsTpl = new QueryableExtensionsTemplate(module.Name, model.Value);
                            var extensionsText = extensionsTpl.TransformText();
                            System.IO.File.WriteAllText($"E:/temp/gentest2/DataAccess/Queries/{model.Key}QueryableExtensions.cs", extensionsText);
                            Console.WriteLine(extensionsText);
                            Console.WriteLine();
                        }
                    }
                    var configTpl = new ModelConfigurationTemplate(module.Name, model.Value);
                    var configText = configTpl.TransformText();
                    System.IO.File.WriteAllText($"E:/temp/gentest2/DataAccess/{model.Key}Configuration.cs", configText);
                    Console.WriteLine(configText);
                    Console.WriteLine();
                }
            }

            var dataAccesTpl = new DataAccessTemplate(module);
            var text2 = dataAccesTpl.TransformText();
            System.IO.File.WriteAllText($"E:/temp/gentest2/Domain/Model/_DataAccess.Ef.cs", text2);
            Console.WriteLine(text2);
            Console.WriteLine();

            var contextTpl = new ContextTemplate(module);
            var contextText = contextTpl.TransformText();
            System.IO.File.WriteAllText($"E:/temp/gentest2/DataAccess/Context.cs", contextText);
            Console.WriteLine(contextText);
            Console.WriteLine();

            var contextFactoryTpl = new ContextFactoryTemplate(module);
            var contextFactoryText = contextFactoryTpl.TransformText();
            System.IO.File.WriteAllText($"E:/temp/gentest2/DataAccess/ContextFactory.cs", contextFactoryText);
            Console.WriteLine(contextFactoryText);
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
