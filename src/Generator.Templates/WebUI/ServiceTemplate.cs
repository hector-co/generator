using Generator.Metadata;
using Humanizer;

namespace Generator.Templates.WebUI;

public class ServiceTemplate
{
    private readonly ModuleDefinition _module;
    private readonly ModelDefinition _model;

    public ServiceTemplate(ModuleDefinition moduleDefinition, ModelDefinition modelDefinition)
    {
        _module = moduleDefinition;
        _model = modelDefinition;
    }

    public string Generate()
    {
        return $$"""
            import { api } from 'src/boot/axios';
            import {
              filterToQueryString,
              QueryType,
              pageToQueryString,
              Result,
              orderByToQueryString,
            } from 'src/common/helpers';
            import config from 'src/common/configuration';
            import { {{_model.Name}} } from '../model/{{_model.Name.Camelize()}}';

            const ApiUrl = '{{_module.Settings.ApiPrefix}}/{{_model.PluralName.Kebaberize()}}';
            const BaseUrl = config.WebApiUrl;

            export default {
              async get(id: number): Promise<Result<{{_model.Name}}>> {
                const response = await api.get<Result<{{_model.Name}}>>(`${ApiUrl}/${id}`, {
                  baseURL: BaseUrl,
                });

                return {
                  data: createModels([response.data.data])[0],
                  meta: response.data.meta,
                };
              },
              async list(query?: QueryType): Promise<Result<Array<{{_model.Name}}>>> {
                const response = await api.get<Result<Array<{{_model.Name}}>>>(
                  `${ApiUrl}?` +
                    filterToQueryString(query?.filter, true) +
                    orderByToQueryString(query?.orderBy, true) +
                    pageToQueryString(query?.page),
                  {
                    baseURL: BaseUrl,
                  }
                );

                return {
                  data: createModels(response.data.data),
                  totalCount: response.data.totalCount,
                  meta: response.data.meta,
                };
              },
              async register(model: any): Promise<Result<{{_model.Name}}>> {
                const response = (await api.post(`${ApiUrl}`, JSON.stringify(model), {
                  baseURL: BaseUrl,
                })) as any;

                return {
                  data: createModels([response.data.data])[0],
                  meta: response.data.meta,
                };
              },
              async update(id: number, model: any): Promise<void> {
                await api.put(`${ApiUrl}/${id}`, JSON.stringify(model), {
                  baseURL: BaseUrl,
                });
              },
              async remove(id: number): Promise<void> {
                await api.delete(`${ApiUrl}/${id}`, {
                  baseURL: BaseUrl,
                });
              },
            };

            function createModels(data: Array<any>): {{_model.Name}}[] {
              if (!data) return [];

              return data.map((d) => new {{_model.Name}}(d));
            }            
            """;
    }
}
