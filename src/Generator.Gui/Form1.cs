using Generator.FilesGeneration;
using Generator.Metadata;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Generator.Gui
{
    public partial class Form1 : Form
    {
        public Form1(string file)
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(file))
                LoadModelsInfo(file);
            else
                SetUIEnabled(false);
        }

        private void LoadModelsInfo(string file)
        {
            textFile.Text = file;
            treeModel.Nodes.Clear();
            var moduleDefinition = JsonConvert.DeserializeObject<ModuleDefinition>(File.ReadAllText(file));

            foreach (var modelName in moduleDefinition.Model.Keys)
            {
                var node = treeModel.Nodes.Add(modelName);

                AddNode(node, "Domain", "d");
                AddNode(node, "Queries", "q");
                AddNode(node, "DataAccessEf", "da");
                AddNode(node, "Api", "a");
            }

            treeModel.ExpandAll();

            UpdateUI(() =>
            {
                SetAllCheckedState(true);
                SetUIEnabled(true);
            });

            static void AddNode(TreeNode parent, string text, string tag)
            {
                var node = parent.Nodes.Add(text);
                node.Tag = tag;
            }
        }

        private void SetAllCheckedState(bool @checked)
        {
            SetMainOptionsChecked(@checked);
            SetTreeNodesChecked(@checked);
        }

        private void SetMainOptionsChecked(bool @checked)
        {
            checkSelectAll.Checked = @checked;

            checkDomain.CheckState = CheckState.Unchecked;
            checkDomain.Checked = @checked;
            checkQueries.CheckState = CheckState.Unchecked;
            checkQueries.Checked = @checked;
            checkDataAccessEf.CheckState = CheckState.Unchecked;
            checkDataAccessEf.Checked = @checked;
            checkApi.CheckState = CheckState.Unchecked;
            checkApi.Checked = @checked;
        }

        private void SetMainOptionChecked(string optionName, bool @checked)
        {
            switch (optionName)
            {
                case "Domain":
                    checkDomain.Checked = @checked;
                    break;
                case "Queries":
                    checkQueries.Checked = @checked;
                    break;
                case "DataAccessEf":
                    checkDataAccessEf.Checked = @checked;
                    break;
                case "Api":
                    checkApi.Checked = @checked;
                    break;
            }

            SetTreeNodeChecked(optionName, @checked);
            SetCheckedAllState();
        }

        private void UpdateMainOptionsCheckedState()
        {
            (int domain, int queries, int dataAccessEf, int api) result = (-1, -1, -1, -1);
            foreach (TreeNode node in treeModel.Nodes)
            {
                foreach (TreeNode childNode in node.Nodes)
                {
                    switch (childNode.Text)
                    {
                        case "Domain":
                            result.domain = GetcheckState(result.domain, childNode.Checked);
                            break;
                        case "Queries":
                            result.queries = GetcheckState(result.queries, childNode.Checked);
                            break;
                        case "DataAccessEf":
                            result.dataAccessEf = GetcheckState(result.dataAccessEf, childNode.Checked);
                            break;
                        case "Api":
                            result.api = GetcheckState(result.api, childNode.Checked);
                            break;
                    }
                }
            }

            SetCheckBoxState(checkDomain, result.domain);
            SetCheckBoxState(checkQueries, result.queries);
            SetCheckBoxState(checkDataAccessEf, result.dataAccessEf);
            SetCheckBoxState(checkApi, result.api);

            SetCheckedAllState();

            static int GetcheckState(int prevState, bool @checked)
            {
                if (prevState == 1) return prevState;
                if (@checked && prevState == 2) return 2;
                if (!@checked && prevState == 0) return 0;
                if (@checked && prevState == -1) return 2;
                if (!@checked && prevState == -1) return 0;
                return 1;
            }

            static void SetCheckBoxState(CheckBox checkBox, int checkState)
            {
                if (checkState == 0)
                    checkBox.Checked = false;
                if (checkState == 1)
                    checkBox.CheckState = CheckState.Indeterminate;
                if (checkState == 2)
                    checkBox.CheckState = CheckState.Checked;
            }
        }

        private void SetCheckedAllState()
        {
            checkSelectAll.Checked =
                checkDomain.Checked && checkDomain.CheckState == CheckState.Checked
                && checkQueries.Checked && checkQueries.CheckState == CheckState.Checked
                && checkDataAccessEf.Checked && checkDataAccessEf.CheckState == CheckState.Checked
                && checkApi.Checked && checkApi.CheckState == CheckState.Checked;
        }

        private void SetTreeNodeChecked(string optionName, bool @checked)
        {
            foreach (TreeNode node in treeModel.Nodes)
            {
                foreach (TreeNode childNode in node.Nodes)
                {
                    if (childNode.Text == optionName)
                        childNode.Checked = @checked;
                }

                var parentChecked = false;
                foreach (TreeNode childNode in node.Nodes)
                {
                    if (childNode.Checked)
                    {
                        parentChecked = true;
                        break;
                    }
                }
                node.Checked = parentChecked;
            }
        }

        private void SetTreeNodesChecked(bool @checked)
        {
            foreach (TreeNode node in treeModel.Nodes)
            {
                node.Checked = @checked;

                foreach (TreeNode child in node.Nodes)
                {
                    child.Checked = @checked;
                }
            }
        }

        private void SelectAll_CheckedChanged(object sender, EventArgs e)
        {
            UpdateUI(() =>
            {
                SetAllCheckedState(checkSelectAll.Checked);
            });
        }

        private void MainOption_CheckedChanged(object sender, EventArgs e)
        {
            UpdateUI(() =>
            {
                SetMainOptionChecked(((CheckBox)sender).Text, ((CheckBox)sender).Checked);
            });
        }

        private void TreeModel_AfterCheck(object sender, TreeViewEventArgs e)
        {
            UpdateUI(() =>
            {
                foreach (TreeNode node in e.Node.Nodes)
                    node.Checked = e.Node.Checked;

                if (e.Node.Parent != null)
                {
                    var parentChecked = false;
                    foreach (TreeNode childNode in e.Node.Parent.Nodes)
                    {
                        if (childNode.Checked)
                        {
                            parentChecked = true;
                            break;
                        }
                    }
                    e.Node.Parent.Checked = parentChecked;
                }

                UpdateMainOptionsCheckedState();
            });
        }

        private void UpdateUI(Action action)
        {
            RemoveEventHandlers();

            action();

            AddEventHandlers();
        }

        private void RemoveEventHandlers()
        {
            checkSelectAll.CheckedChanged -= SelectAll_CheckedChanged;

            checkDomain.CheckedChanged -= MainOption_CheckedChanged;
            checkQueries.CheckedChanged -= MainOption_CheckedChanged;
            checkDataAccessEf.CheckedChanged -= MainOption_CheckedChanged;
            checkApi.CheckedChanged -= MainOption_CheckedChanged;

            treeModel.AfterCheck -= TreeModel_AfterCheck;
        }

        private void AddEventHandlers()
        {
            checkSelectAll.CheckedChanged += SelectAll_CheckedChanged;

            checkDomain.CheckedChanged += MainOption_CheckedChanged;
            checkQueries.CheckedChanged += MainOption_CheckedChanged;
            checkDataAccessEf.CheckedChanged += MainOption_CheckedChanged;
            checkApi.CheckedChanged += MainOption_CheckedChanged;

            treeModel.AfterCheck += TreeModel_AfterCheck;
        }

        private void SetUIEnabled(bool enabled)
        {
            checkSelectAll.Enabled = enabled;

            checkDomain.Enabled = enabled;
            checkQueries.Enabled = enabled;
            checkDataAccessEf.Enabled = enabled;
            checkApi.Enabled = enabled;

            treeModel.Enabled = enabled;

            checkForceRegen.Enabled = enabled;
            buttonGenerate.Enabled = enabled;
        }

        private Dictionary<string, TemplateGenerationOption> GetGenerationOptions()
        {
            var result = new Dictionary<string, TemplateGenerationOption>();

            foreach (TreeNode node in treeModel.Nodes)
            {
                var tplOption = new TemplateGenerationOption();
                foreach (TreeNode childNode in node.Nodes)
                {
                    if (childNode.Checked)
                    {
                        switch (childNode.Text)
                        {
                            case "Domain":
                                tplOption.Domain = true;
                                break;
                            case "Queries":
                                tplOption.Query = true;
                                break;
                            case "DataAccessEf":
                                tplOption.DataAccessEf = true;
                                break;
                            case "Api":
                                tplOption.Api = true;
                                break;
                        }
                    }
                }
                if (tplOption.AnyChecked())
                    result.Add(node.Text, tplOption);
            }

            return result;
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textFile.Text))
                return;

            new FileGenerator(textFile.Text, GetGenerationOptions(), Path.GetDirectoryName(textFile.Text), checkForceRegen.Checked)
                .Generate();

            MessageBox.Show(this, "Generation completed", "Generator", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Close();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            if (openFile.ShowDialog() == DialogResult.OK)
                LoadModelsInfo(openFile.FileName);
        }
    }
}