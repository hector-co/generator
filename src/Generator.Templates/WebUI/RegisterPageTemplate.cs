﻿using Generator.Metadata;
using Humanizer;
using System;
using System.Linq;

namespace Generator.Templates.WebUI;

public class RegisterPageTemplate
{
    private readonly ModuleDefinition _module;
    private readonly ModelDefinition _model;

    public RegisterPageTemplate(ModuleDefinition moduleDefinition, ModelDefinition modelDefinition)
    {
        _module = moduleDefinition;
        _model = modelDefinition;
    }

    public string Generate()
    {
        return $$$"""
            <template>
              <div class="{{{_model.PluralName.Kebaberize()}}}-register">
                <div class="row justify-center">
                  <div class="col col-sm-8 col-md-6">
                    <q-form @submit="onSubmit">
                      <div class="row">
                        <div class="col-12">
                          <span class="text-h5">Registrar {{{_model.Name.Titleize()}}}</span>
                          {{{GetHtmlFields()}}}
                        </div>
                        <div class="col-12 text-right">
                          <q-btn label="Aceptar" type="submit" color="primary" />
                          <q-btn
                            label="Cancelar"
                            color="primary"
                            outline
                            class="q-ml-sm"
                            :to="{ name: '{{{_model.PluralName.Kebaberize()}}}-list' }"
                          />
                        </div>
                      </div>
                    </q-form>
                  </div>
                </div>
              </div>
            </template>
            <script lang="ts" setup>{{{(HasRefs() ? $"{Environment.NewLine}import {{ onMounted, ref }} from 'vue';" : "")}}}
            import { useRouter } from 'vue-router';
            import { useForm } from 'vee-validate';
            import * as yup from 'yup';
            import notifier from 'src/common/notifier';
            {{{GetServiceImports()}}}

            const router = useRouter();
            {{{GetRels()}}}
            {{{GetValidationScheme()}}}

            const { defineField, handleSubmit } = useForm({
              validationSchema: validationSchema,
            });

            const quasarConfig = (state: any) => ({
              props: {
                error: !!state.errors[0],
                'error-message': state.errors[0],
              },
            });

            {{{GetFields()}}}

            const onSubmit = handleSubmit(
              async ({{{GetSubmitArgs()}}}) => {
                try {
                  await {{{_model.Name.Camelize()}}}Service.register({{{GetSubmitValues()}}});
                  notifier.success('Registro agregado');
                  router.push({ name: '{{{_model.PluralName.Kebaberize()}}}-list' });
                } catch {}
              }
            );
            {{{GetRelsInit()}}}</script>
            
            <!--
              {
                name: '{{{_model.PluralName.Kebaberize()}}}-register',
                path: '/{{{_model.PluralName.Kebaberize()}}}/register',
                component: () => import('src/modules/{{{_module.Name.Camelize()}}}/pages/{{{_model.PluralName.Camelize()}}}/RegisterPage.vue'),
              },
            -->
            """;
    }

    public string GetServiceImports()
    {
        var imports = $"import {_model.Name.Camelize()}Service from '../../services/{_model.Name.Camelize()}Service'{Environment.NewLine}";
        foreach (var model in _model.GetRelatedEntities(_module).Where(p => !p.IsOwned).Select(p => p.Model).Distinct())
        {
            if (model.IsExternal)
            {
                imports += $"import {model.Name.Camelize()}Service from '../../../{model.External.Camelize()}/services/{model.Name.Camelize()}Service';{Environment.NewLine}";
            }
            else
            {
                imports += $"import {model.Name.Camelize()}Service from '../../services/{model.Name.Camelize()}Service';{Environment.NewLine}";
            }
        }
        return imports.TrimEnd();
    }

    public string GetValidationScheme()
    {
        var sch = "const validationSchema = yup.object({" + Environment.NewLine;

        return sch + "});";
        //category: yup.object().shape({
        //  id: yup.number().required(),
        //}),
        //name: yup.string().required().label('Nombre'),  
    }

    public string GetRels()
    {
        var rels = Environment.NewLine;
        foreach (var model in _model.GetRelatedEntities(_module).Where(p => !p.IsOwned).Select(p => p.Model).Distinct())
        {
            rels += $"const {model.PluralName.Camelize()} = ref<{{ value: number, label: string}}[]>([]);{Environment.NewLine}";
        }
        return rels == Environment.NewLine ? string.Empty : rels;
    }

    public string GetRelsInit()
    {
        if (!HasRefs())
            return string.Empty;

        var rels = Environment.NewLine + "onMounted(async () => {" + Environment.NewLine;
        foreach (var model in _model.GetRelatedEntities(_module).Where(p => !p.IsOwned).Select(p => p.Model).Distinct())
        {
            rels += $$"""
                  {{model.PluralName.Camelize()}}.value = (await {{model.Name.Camelize()}}Service.list()).data.map((item: any) => ({
                    value: item.id,
                    label: item.name,
                  }));{{Environment.NewLine}}
                """;
        }
        return rels + "});" + Environment.NewLine;
    }

    public string GetHtmlFields()
    {
        var fields = Environment.NewLine;
        foreach (var property in _model.Properties.Values)
        {
            if (property.IsEntityType)
                fields += $"""
                              <q-select 
                                label="{property.UI.Label}"
                                v-model="{property.Name.Camelize()}"
                                v-bind="{property.Name.Camelize()}Props"
                                :options="{property.CastTargetType<ModelTypeDefinition>().Model.PluralName.Camelize()}"
                                map-options
                                emit-value
                              />{Environment.NewLine}
                """;
            else
                fields += $"""
                              <q-input
                                label="{property.UI.Label}"
                                v-model="{property.Name.Camelize()}"
                                v-bind="{property.Name.Camelize()}Props"
                              />{Environment.NewLine}
                """;
        }
        return fields.Trim();
    }

    public string GetFields()
    {
        var fields = string.Empty;
        foreach (var property in _model.Properties.Values)
        {
            if (property.IsEntityType)
                fields += $@"const [{property.Name.Camelize()}, {property.Name.Camelize()}Props] = defineField('{property.Name.Camelize()}.id', quasarConfig);{Environment.NewLine}";
            else
                fields += $@"const [{property.Name.Camelize()}, {property.Name.Camelize()}Props] = defineField('{property.Name.Camelize()}', quasarConfig);{Environment.NewLine}";
        }
        return fields.Trim();
    }

    public string GetSubmitArgs()
    {
        return $"{{ {_model.Properties.Values.Select(p => p.Name.Camelize()).Aggregate((a, b) => $"{a}, {b}")} }}";
    }

    public string GetSubmitValues()
    {
        var values = "{" + Environment.NewLine;
        foreach (var property in _model.Properties.Values)
        {
            if (property.IsEntityType)
                values += $@"        {property.Name.Camelize()}Id: {property.Name.Camelize()}.id,{Environment.NewLine}";
            else
                values += $@"        {property.Name.Camelize()},{Environment.NewLine}";
        }
        return values.TrimEnd() + Environment.NewLine + "      }";
    }

    private bool HasRefs()
    {
        return _model.GetRelatedEntities(_module).Any(p => !p.IsOwned);
    }
}
