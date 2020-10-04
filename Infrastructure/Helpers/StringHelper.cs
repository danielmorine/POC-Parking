using System;

namespace Infrastructure.Helpers
{
    public static class StringHelper
    {
        public static void StringNullOrEmpty(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new Exception("Todos os campos são obrigatórios!");
            }
        }
    }
}
