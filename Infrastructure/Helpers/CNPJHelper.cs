namespace Infrastructure.Helpers
{
    public class CNPJHelper
    {
        public bool IsCNPJ(string cnpj)
        {
            int[] multiple1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiple2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int sum = 0;
            int rest;
            string digit;
            string tempCnpj;
            cnpj = Clean(cnpj);
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            for (int i = 0; i < 12; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multiple1[i];

            rest = (sum % 11);
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit = rest.ToString();
            tempCnpj += digit;
            sum = 0;

            for (int i = 0; i < 13; i++)
                sum += int.Parse(tempCnpj[i].ToString()) * multiple2[i];
            rest = (sum % 11);
            if (rest < 2)
                rest = 0;
            else
                rest = 11 - rest;

            digit += rest.ToString();
            return cnpj.EndsWith(digit);
        }

        public string Clean(string cnpj)
        {
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            return cnpj;
        }
    }
}
