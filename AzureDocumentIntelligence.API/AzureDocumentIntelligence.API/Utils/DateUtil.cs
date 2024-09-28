using Azure.AI.FormRecognizer.DocumentAnalysis;

namespace AzureDocumentIntelligence.API.Utils
{
    public static class DateUtil
    {
        public static DateOnly? ParseDate(DocumentField field)
        {
            if (field.FieldType == DocumentFieldType.Date)
            {
                var date = field.Value.AsDate();
                return new DateOnly(date.Year, date.Month, date.Day);
            }

            return null;
        }
    }
}
