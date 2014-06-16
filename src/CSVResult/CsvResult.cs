using System;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Mvc;
using CsvHelper;

namespace CsvResult
{
    public class CsvResult : ActionResult
    {
        public string ContentType { get; set; }
        public IEnumerable Data { get; set; }

        public CsvResult(IEnumerable data)
        {
            this.Data = data;
            ContentType = "text/csv";
        }


        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;
            if (string.IsNullOrEmpty(this.ContentType))
            {
                response.ContentType = "text/csv";
            }
            else
            {
                response.ContentType = this.ContentType;
            }
            if (this.Data != null)
            {
                using (var ms = new MemoryStream())
                {
                    using (var textWriter = new StreamWriter(ms))
                    {
                        var csvSerializer = new CsvWriter(textWriter);
                        csvSerializer.WriteRecords(Data); //write to the memory stream

                        using (var textReader = new StreamReader(ms)) //read back from the memory stream TODO://clean this up
                        {
                            response.Write(textReader.ReadToEnd());
                        }
                    }
                }

            }

        }
    }
}
