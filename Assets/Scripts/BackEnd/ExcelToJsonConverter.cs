using UnityEngine;
using System.IO;
using ExcelDataReader;
using System.Data;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;

public class ExcelToJsonConverter : MonoBehaviour
{
    public string excelFilePath = "Assets/YourExcelFile.xlsx";
    public string jsonFilePath = "Assets/ConvertedFile.json";

    void Start()
    {
        ConvertExcelToJson();
    }

    private void ConvertExcelToJson()
    {
        //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        Dictionary<string, object> allSheets = new Dictionary<string, object>();

        using (FileStream stream = File.Open(excelFilePath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                var result = reader.AsDataSet();

                // 모든 sheet를 순회
                for (int i = 0; i < result.Tables.Count; i++)
                {
                    DataTable dataTable = result.Tables[i];
                    string sheetName = dataTable.TableName;

                    // DataTable을 List<Dictionary<string, object>>로 변환
                    List<Dictionary<string, object>> tableData = new List<Dictionary<string, object>>();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        Dictionary<string, object> rowData = new Dictionary<string, object>();
                        foreach (DataColumn col in dataTable.Columns)
                        {
                            rowData[col.ColumnName] = row[col];
                        }
                        tableData.Add(rowData);
                    }

                    allSheets[sheetName] = tableData;
                }
            }
        }

        // 모든 sheet 데이터를 JSON으로 변환
        string jsonData = JsonConvert.SerializeObject(allSheets, Formatting.Indented);
        File.WriteAllText(jsonFilePath, jsonData);

        Debug.Log("Conversion completed. JSON file saved at: " + jsonFilePath);
    }
}