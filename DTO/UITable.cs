
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JustSupportSystem.DTO
{
    public class UITable
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalRecords { get; set; } = 0;
        public bool EnableActions { get; set; } = true;
        public UIButton? AddButton { get; set; }
        public UIButton? EditButton { get; set; }
        public UIButton? DeleteButton { get; set; }
        public UIButton? ViewButton { get; set; }
        public UITableData? Data { get; set; }
        public string PaginationUrl { get; set; }
        public string? NoDataMessage { get; set; } = "No records found.";
        public string SearchUrl { get; set; } = string.Empty;

        public UITable()
        {
            PaginationUrl = string.Empty;
        }

        public static UITable CreateNewTable<T>(string title,Dictionary<int, string> headers, List<T> data)
        {
            UITable table = new UITable().AddHeader(headers).AddDataFromDTO(data);
            table.Title = title;
            return table;
        }
        public static UITable CreateNewTable<T>(string title, List<T> data)
        {
            UITable table = new UITable().AddHeaderFromDTO(data).AddDataFromDTO(data);
            table.Title = title;
            return table;
        }

        public UITable AddHeader(Dictionary<int, string> headers)
        {
            if (Data == null)
            {
                Data = new UITableData();
            }
            Data.Headers = headers;
            return this;
        }

        public UITable AddHeaderFromDTO<T>(List<T> data)
        {
            if (Data == null)
            {
                Data = new UITableData();
            }
            Type classType = typeof(T);
            PropertyInfo[] properties = classType.GetProperties();
            Data.Headers = new Dictionary<int, string>();
            int i = 0;
            foreach (var d in properties)
            {
                if (data.Count > 0)
                {
                    var valueInternal = d.GetValue(data[0]);
                    if (valueInternal != null)
                    {
                        Data.Headers.Add(i++, d.Name);
                    }
                }
                else
                {
                    Data.Headers.Add(i++, d.Name);
                }
                
            }
            return this;
        }

        public UITable AddDataFromDTO<T>(List<T> data)
        {
            if(Data==null || Data.Headers == null)
            {
                return this;
            }
            Type classType = typeof(T);
            if (this.Data == null)
            {
                this.Data = new UITableData();
            }
            this.Data.Rows = new List<Dictionary<string, string>>();
            PropertyInfo[] properties = classType.GetProperties();

            foreach (var d in data)
            {
                var row = new Dictionary<string, string>();
                foreach(var head in Data.Headers)
                {
                    var prop = properties.FirstOrDefault(p => p.Name == head.Value);
                    if (prop != null) 
                    {
                        var valueInternal = prop.GetValue(d);
                        if (valueInternal != null)
                        {
                            row.Add(head.Value, valueInternal.ToString());
                          
                        }
                    }
                }
                if (row.Count > 0)
                {
                    this.Data.Rows.Add(row);
                }
            }
            return this;
        }

        public UITable AddDataList(List<Dictionary<string, string>> data)
        {
            if (Data == null)
            {
                Data = new UITableData();
            }
            Data.Rows = data;
            return this;
        }
        public UITable AddRow(Dictionary<string, string> row)
        {
            if (Data == null)
            {
                Data = new UITableData();
            }
            if (Data.Rows == null)
            {
                Data.Rows = new List<Dictionary<string, string>>();
            }
            Data.Rows.Add(row);
            return this;
        }

        public UITable AddTableInfo(string title, string desc)
        {
            Title = title;
            Description = desc;
            return this;
        }

        public UITable AddPagination(int pageNo, int pageSize, int totalRecords, string paginationUrl)
        {
            PageNo = pageNo;
            PageSize = pageSize;
            TotalRecords = totalRecords;
            PaginationUrl = paginationUrl;
            return this;
        }

        public UITable AddActionButtons(UIButton addButton, UIButton editButton, UIButton deleteButton)
        {
            AddButton = addButton;
            EditButton = editButton;
            DeleteButton = deleteButton;
            DeleteButton.Type = UIButtonType.Secondary;
            return this;
        }

        public UITable AddActionButtons(string addUrl, string editUrl = "", string viewUrl = "", string deleteUrl = "")
        {
            if (!string.IsNullOrEmpty(addUrl))
            {
                AddButton = new UIButton
                {
                    Title = "Add",
                    Url = addUrl
                };
            }
            if (!string.IsNullOrEmpty(editUrl))
            {
                EditButton = new UIButton
                {
                    Title = "Edit",
                    Url = editUrl
                };
            }
            if (!string.IsNullOrEmpty(deleteUrl))
            {
                DeleteButton = new UIButton
                {
                    Title = "Delete",
                    Url = deleteUrl,
                    Type = UIButtonType.Secondary
                };
            }
            if (!string.IsNullOrEmpty(viewUrl))
            {
                ViewButton = new UIButton
                {
                    Title = "View",
                    Url = viewUrl,
                    Type = UIButtonType.Secondary
                };
            }
            return this;
        }

        public UITable AddData(UITableData data)
        {
            Data = data;
            return this;
        }
    }
}
