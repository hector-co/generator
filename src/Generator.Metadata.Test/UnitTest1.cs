using FluentAssertions;
using System.Linq;
using Xunit;

namespace Generator.Metadata.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var str = @"
  {
    'models': {
      'Model1': {
        'properties': {
          'Prop1': {
            'typeName': 'string'
          }
        }
      }
    }
  }";

            var module = Newtonsoft.Json.JsonConvert.DeserializeObject<ModuleDefinition>(str);
            module.Init();

            module.Models.ContainsKey("Model1").Should().BeTrue();
            module.Models["Model1"].Name.Should().Be("Model1");
            module.Models["Model1"].Properties.Count.Should().Be(1);
            module.Models["Model1"].Properties.ContainsKey("Prop1").Should().BeTrue();
            module.Models["Model1"].Properties["Prop1"].Name.Should().Be("Prop1");
            module.Models["Model1"].Properties["Prop1"].TypeName.Should().Be("string");
        }

        [Fact]
        public void Test2()
        {
            var str = @"
  {
    'models': {
      'Model1': {
        'properties': {
          'Prop1': {
            'typeName': 'string'
          }
        }
      },
      'Model2': {
        'properties': {
          'Prop1': {
            'typeName': 'string'
          },
          'Model1Rel': {
            'typeName': 'Model1'
          }
        }
      }
    }
  }";

            var module = Newtonsoft.Json.JsonConvert.DeserializeObject<ModuleDefinition>(str);
            module.Init();

            module.Models.ContainsKey("Model2").Should().BeTrue();
            module.Models["Model2"].Properties.Count.Should().Be(2);
            module.Models["Model2"].Properties.ContainsKey("Model1Rel").Should().BeTrue();
            module.Models["Model2"].Properties["Model1Rel"].TypeName.Should().Be("Model1");
            module.Models["Model2"].Properties["Model1Rel"].TargetTypes.Any(p => p is ModelTypeDefinition).Should().BeTrue();
            module.Models["Model2"].Properties["Model1Rel"].TargetType.Cast<ModelTypeDefinition>().Should().NotBeNull();
        }

        [Fact]
        public void Test3()
        {
            var str = @"
  {
    'models': {
      'Model1': {
        'properties': {
          'Prop1': {
            'typeName': 'string'
          }
        }
      },
      'Model2': {
        'properties': {
          'Prop1': {
            'typeName': 'string'
          },
          'Model1Rels': {
            'typeName': 'List<Model1>'
          }
        }
      }
    }
  }";

            var module = Newtonsoft.Json.JsonConvert.DeserializeObject<ModuleDefinition>(str);
            module.Init();

            module.Models["Model2"].Properties.Count.Should().Be(2);
            module.Models["Model2"].Properties.ContainsKey("Model1Rels").Should().BeTrue();
            module.Models["Model2"].Properties["Model1Rels"].InternalTypeName.Should().Be("List<:T0:>");
            module.Models["Model2"].Properties["Model1Rels"].IsCollection.Should().BeTrue();
            module.Models["Model2"].Properties["Model1Rels"].IsGeneric.Should().BeTrue();
            module.Models["Model2"].Properties["Model1Rels"].TargetTypes.Any(p => p is ModelTypeDefinition).Should().BeTrue();
            module.Models["Model2"].Properties["Model1Rels"].TargetType.Cast<ModelTypeDefinition>().Should().NotBeNull();
        }

        [Fact]
        public void Test4()
        {
            var str = @"
  {
    'models': {
      'Model1': {
        'properties': {
          'Prop1': {
            'typeName': 'Dictionary<string, int>'
          }
        }
      }
    }
  }";

            var module = Newtonsoft.Json.JsonConvert.DeserializeObject<ModuleDefinition>(str);
            module.Init();

            module.Models["Model1"].Properties.Count.Should().Be(1);
            module.Models["Model1"].Properties["Prop1"].InternalTypeName.Should().Be("Dictionary<:T0:, :T1:>");
            module.Models["Model1"].Properties["Prop1"].IsCollection.Should().BeFalse();
            module.Models["Model1"].Properties["Prop1"].IsGeneric.Should().BeTrue();
        }

        [Fact]
        public void Test5()
        {
            var str = @"
  {
    'models': {
      'Model1': {
        'properties': {
          'Prop1': {
            'typeName': 'string'
          }
        },
        'isRoot': false
      },
      'Model2': {
        'properties': {
          'Prop1': {
            'typeName': 'string'
          },
          'Model1Rels': {
            'typeName': 'List<Model1>'
          }
        }
      }
    }
  }";

            var module = Newtonsoft.Json.JsonConvert.DeserializeObject<ModuleDefinition>(str);
            module.Init();

            module.Models["Model2"].Properties.Count.Should().Be(2);
            module.Models["Model2"].Properties.ContainsKey("Model1Rels").Should().BeTrue();
            module.Models["Model2"].Properties["Model1Rels"].TargetTypes.Any(p => p is ModelTypeDefinition).Should().BeTrue();
            module.Models["Model2"].Properties["Model1Rels"].TargetType.Cast<ModelTypeDefinition>().Should().NotBeNull();
            module.Models["Model1"].Parent.Should().NotBeNull();
            module.Models["Model1"].Parent.Should().Be(module.Models["Model2"]);
        }

        [Fact]
        public void Test6()
        {
            var str = @"
  {
    'models': {
      'Model1': {
        'properties': {
          'Prop1': {
            'typeName': 'string'
          },
          'Prop2': {
            'typeName': 'Enum1'
          }
        }
      }
    },
    'enums': {
      'Enum1': {
        'values': [ 'V1', 'V2' ]
      }
    }
  }";

            var module = Newtonsoft.Json.JsonConvert.DeserializeObject<ModuleDefinition>(str);
            module.Init();

            module.Enums.Should().NotBeEmpty();
            module.Enums.ContainsKey("Enum1").Should().BeTrue();
            module.Enums["Enum1"].Name.Should().Be("Enum1");
            module.Enums["Enum1"].Values.Count.Should().Be(2);
            module.Models["Model1"].Properties.Count.Should().Be(2);
            module.Models["Model1"].Properties.ContainsKey("Prop2").Should().BeTrue();
            module.Models["Model1"].Properties["Prop2"].Name.Should().Be("Prop2");
            module.Models["Model1"].Properties["Prop2"].TypeName.Should().Be("Enum1");
            module.Models["Model1"].Properties["Prop2"].TargetTypes.Any(p => p is EnumTypeDefinition).Should().BeTrue();
        }

        [Fact]
        public void Test7()
        {
            var str = @"
  {
    'models': {
      'Model1': {
        'properties': {
          'Prop1': {
            'typeName': 'int?'
          },
          'Prop2': {
            'typeName': 'Enum1?'
          }
        }
      }
    },
    'enums': {
      'Enum1': {
        'values': [ 'V1', 'V2' ]
      }
    }
  }";

            var module = Newtonsoft.Json.JsonConvert.DeserializeObject<ModuleDefinition>(str);
            module.Init();

            module.Enums.Should().NotBeEmpty();
            module.Enums.ContainsKey("Enum1").Should().BeTrue();
            module.Enums["Enum1"].Name.Should().Be("Enum1");
            module.Enums["Enum1"].Values.Count.Should().Be(2);
            module.Models["Model1"].Properties["Prop1"].TargetType.Name.Should().Be("int");
            module.Models["Model1"].Properties["Prop1"].TargetType.IsNullable.Should().BeTrue();
            module.Models["Model1"].Properties["Prop2"].TargetType.Name.Should().Be("Enum1");
            module.Models["Model1"].Properties["Prop2"].TargetType.IsNullable.Should().BeTrue();
            module.Models["Model1"].Properties["Prop2"].TargetTypes.Any(p => p is EnumTypeDefinition).Should().BeTrue();
        }

        [Fact]
        public void Test8()
        {
            var str = @"
  {
    'models': {
      'Model1': {
        'properties': {
          'Prop1': {
            'typeName': 'string'
          }
        },
        'isRoot': false
      },
      'Model2': {
        'properties': {
          'Model1Rel': {
            'typeName': 'Model1?'
          }
        }
      }
    }
  }";

            var module = Newtonsoft.Json.JsonConvert.DeserializeObject<ModuleDefinition>(str);
            module.Init();

            module.Models["Model2"].Properties["Model1Rel"].TargetType.Cast<ModelTypeDefinition>().Should().NotBeNull();
            module.Models["Model2"].Properties["Model1Rel"].TargetType.IsNullable.Should().BeTrue();
            module.Models["Model2"].Properties["Model1Rel"].TargetType.Name.Should().Be("Model1");
        }
    }
}
