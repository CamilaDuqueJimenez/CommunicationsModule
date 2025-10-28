namespace DomoNow.Communications.Application.Commons
{
    public class Constants
    {
        public const string GENERAL_ERROR = "Ocurrio un error en el servidor";
        public static string GENERIC_MESSAGE_EXCEPTION(string mensaje) => $"Se presentó un error inesperado: {mensaje ?? ""}.";
        public const string FAILURE = "Failure";
        public static string CITY_BY_DEPARTMENT_NOT_FOUND => $"No se encontraron ciudades para el departamento.";
    }
    public class GeneralConstants
    {
        public const string EXISTING_TRANSACTION = "Ya existe una transacción activa.";
        public const string NON_EXISTENT_TRANSACTION = "No hay una transacción activa para confirmar.";

        public const string ERROR_HEADER_INVALID = "El header '{0}' es obligatorio.";
        public const string ERROR_HEADER_MISSING = "El header '{0}' debe ser un dato válido.";

        public const string ERROR_QUERIES_INVALID = "El query '{0}' es obligatorio.";
        public const string ERROR_QUERIES_MISSING = "El query '{0}' debe ser un dato válido.";
    }
}
