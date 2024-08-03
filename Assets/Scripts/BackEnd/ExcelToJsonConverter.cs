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

    private void ConvertExcelToJson()
    {
        //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        using (FileStream stream = File.Open(excelFilePath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                DataSet result = reader.AsDataSet();
                DataTable dataTable = result.Tables[0];

                string jsonData = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
                File.WriteAllText(jsonFilePath, jsonData);

                Debug.Log("Conversion completed. JSON file saved at: " + jsonFilePath);
            }
        }
    }
}