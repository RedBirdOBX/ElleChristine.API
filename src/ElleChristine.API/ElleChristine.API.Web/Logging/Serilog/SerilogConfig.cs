using NpgsqlTypes;
using Serilog.Sinks.PostgreSQL.ColumnWriters;
using Serilog.Sinks.PostgreSQL;

namespace ElleChristine.API.Web.Logging.Serilog
{
    public static class SerilogConfig
    {
        public static IDictionary<string, ColumnWriterBase> ColumnWriters = BuildColumns();

        private static IDictionary<string, ColumnWriterBase> BuildColumns()
        {
            IDictionary<string, ColumnWriterBase> columnWriters = new Dictionary<string, ColumnWriterBase>
            {
                { "message", new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
                { "message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text) },
                { "level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
                { "raise_date", new TimestampColumnWriter(NpgsqlDbType.TimestampTz) },
                { "exception", new ExceptionColumnWriter(NpgsqlDbType.Text) },
                { "properties", new LogEventSerializedColumnWriter(NpgsqlDbType.Jsonb) },
                { "props_test", new PropertiesColumnWriter(NpgsqlDbType.Jsonb) },
                { "machine_name", new SinglePropertyColumnWriter("MachineName", PropertyWriteMethod.ToString, NpgsqlDbType.Text, "l") }
            };
            return columnWriters;
        }
    }
}
