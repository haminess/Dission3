using UnityEngine;
using System.IO;
using ExcelDataReader;
using System.Data;
using Newtonsoft.Json;
using System.Text;

public class ExcelToJsonConverter : MonoBehaviour
{
    public string excelFilePath = "Assets/YourExcelFile.xlsx";
    public string jsonFilePath = "Assets/ConvertedFile.json";

    void Start()
    {
        ConvertExcelToJson();
    }

    void ConvertExcelToJson()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        using (var stream = File.Open(excelFilePath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                var result = reader.AsDataSet();
                var dataTable = result.Tables[0];

                var jsonData = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
                File.WriteAllText(jsonFilePath, jsonData);

                Debug.Log("Conversion completed. JSON file saved at: " + jsonFilePath);
            }
        }
    }
}