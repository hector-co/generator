using Generator.Metadata;
using Humanizer;
using System;
using System.Linq;

namespace Generator.Templates.WebUI;

public class IndexPageTemplate
{
    private readonly ModuleDefinition _module;
    private readonly ModelDefinition _model;

    public IndexPageTemplate(ModuleDefinition moduleDefinition, ModelDefinition modelDefinition)
    {
        _module = moduleDefinition;
        _model = modelDefinition;
    }

    public string Generate()
    {
        return $$$"""
            <template>
              <div class="{{{_model.PluralName.Kebaberize()}}}-list">
                <q-table
                  title="{{{_model.PluralName}}}"
                  flat
                  :loading="loading"
                  :rows="model"
                  :columns="columns"
                  row-key="id"
                  v-model:pagination="pagination"
                  @request="onRequest"
                  binary-state-sort
                >
                  <template v-slot:top>
                    <div class="col-3 q-table__title">{{{_model.PluralName.Titleize()}}}</div>
                    <q-space />
                    <q-btn icon="add" class="q-ml-md" label="Nuevo" />
                    <q-btn @click="refresh" icon="refresh" class="q-ml-md" />
                  </template>

                  <template v-slot:header="props">
                    <q-tr :props="props">
                      <q-th v-for="col in props.cols" :key="col.name" :props="props">
                        {{ col.label }}
                      </q-th>
                      <q-th style="width: 300px" />
                    </q-tr>
                  </template>
                  <template v-slot:body="props">
                    <q-tr :props="props">
                      <q-td v-for="col in props.cols" :key="col.name" :props="props">
                        {{col.value}}
                      </q-td>
                      <q-td auto-width class="text-right">
                        <q-btn
                          size="sm"
                          icon="edit"
                          class="q-mr-sm"
                        />
                        <q-btn @click="remove(props.row)" size="sm" icon="delete" />
                      </q-td>
                    </q-tr>
                  </template>
                </q-table>
              </div>
            </template>
            <script lang="ts" setup>
            import { ref, onMounted } from 'vue';
            import { QueryType } from 'src/common/helpers';
            import { useQuasar, QTableColumn } from 'quasar';
            import notifier from 'src/common/notifier';
            import {{{_model.Name.Camelize()}}}Service from '../../services/{{{_model.Name.Camelize()}}}Service';
            import { {{{_model.Name}}} } from '../../model/{{{_model.Name.Camelize()}}}';
            
            const $q = useQuasar();

            const model = ref<{{{_model.Name}}}[]>([]);
            const columns: QTableColumn[] = [
            {{{GetColumns()}}}
            ];
            const loading = ref(true);

            const pagination = ref({
              sortBy: 'id',
              descending: false,
              page: 1,
              rowsPerPage: 10,
              rowsNumber: 0,
            });

            const onRequest = async (props: any) => {
              loading.value = true;

              const query: QueryType = {
                orderBy: {
                  property: props.pagination.sortBy,
                  direction: props.pagination.descending ? 'desc' : 'asc',
                },
                page: {
                  size: props.pagination.rowsPerPage,
                  number: props.pagination.page,
                },
              };

              try {
                const result = await {{{_model.Name.Camelize()}}}Service.list(query);
                model.value = result.data;
                pagination.value.rowsNumber = result.totalCount ?? 0;
                pagination.value.sortBy = props.pagination.sortBy;
                pagination.value.page = props.pagination.page;
                pagination.value.rowsPerPage = props.pagination.rowsPerPage;
                pagination.value.descending = props.pagination.descending;
              } finally {
                loading.value = false;
              }
            };
            
            const remove = (obj: {{{_model.Name}}}) => {
              $q.dialog({
                title: 'Confirmar',
                message: `¿Eliminar el registro '${obj.id}'?`,
                cancel: true,
                persistent: true,
              }).onOk(async () => {
                await {{{_model.Name.Camelize()}}}Service.remove(obj.id);
                notifier.success('Registro eliminado');
                await refresh();
              });
            };

            const refresh = async () => {
              await onRequest({ pagination: pagination.value });
            };

            onMounted(async () => {
              await refresh();
            });
            </script>

            <!--
              {
                name: '{{{_model.PluralName.Kebaberize()}}}-list',
                path: '/{{{_model.PluralName.Kebaberize()}}}',
                component: () => import('src/modules/{{{_module.Name.Camelize()}}}/pages/{{{_model.PluralName.Camelize()}}}/IndexPage.vue'),
              },
            -->
            """;
    }

    private string GetColumns()
    {
        var columns = $$"""
                  {
                    name: 'id',
                    label: 'Id',
                    field: 'id',
                    align: 'left',
                    sortable: true,
                    headerStyle: 'width: 100px',
                  },{{Environment.NewLine}}
                """; ;
        foreach (var property in _model.Properties.Values.OrderBy(p => p.UI.Order))
        {
            if (property.UI.HideInList)
                continue;

            columns += $$"""
                  {
                    name: {{property.UI.NameEval}},
                    label: '{{property.UI.Label}}',
                    field: {{property.UI.FieldEval}},
                    align: '{{property.UI.Align}}',
                    sortable: {{(property.UI.Sortable ? "true" : "false")}},
                    headerStyle: '{{property.UI.HeaderStyle}}',
                  },{{Environment.NewLine}}
                """;
        }
        return columns.TrimEnd();
    }
}
