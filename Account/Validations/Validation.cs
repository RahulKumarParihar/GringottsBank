using System;

namespace BankLibrary.Validations
{
    public class Validation
    {
        public static bool ValidateRequired(object entity)
        {
            if (entity == null) return false;
            if (entity.ToString().Length == 0) return false;
            return true;
        }

        public static bool ValidateMaxLength(object entity, int maxLength)
        {
            if (entity == null) return true;
            if (entity.ToString().Length > maxLength) return false;
            return true;
        }

        public static bool ValidateMinLength(object entity, int minLength)
        {
            if (entity == null) return false;
            if (entity.ToString().Length < minLength) return false;
            return true;
        }

        public static bool ValidateGreaterThanZero(object entity)
        {
            if (entity == null) return false;

            if (IsInteger(entity) == false) return false;

            int test = Convert.ToInt32(entity);
            if (test < 1) return false;

            return true;
        }

        public static bool ValidateMatchString(object entity1, object entity2)
        {
            if (entity1 == null && entity2 == null) return true;
            if (entity1 == null && entity2 != null) return false;
            if (entity1 != null && entity2 == null) return false;

            if (entity1.ToString() != entity2.ToString()) return false;

            return true;
        }

        public static bool ValidateDecimalGreaterThanZero(object entity)
        {
            if (entity == null) return false;

            if (IsDecimal(entity) == false) return false;

            decimal test = Convert.ToDecimal(entity);
            if (test < 1) return false;

            return true;
        }

        public static bool ValidateDecimalIsNotZero(object entity)
        {
            if (entity == null) return false;

            if (IsDecimal(entity) == false) return false;

            decimal test = Convert.ToDecimal(entity);
            if (test == 0) return false;

            return true;
        }

        public static bool IsInteger(object entity)
        {
            if (entity == null) return false;

            int result;
            return int.TryParse(entity.ToString(), out result);
        }

        public static bool IsDecimal(object entity)
        {
            if (entity == null) return false;

            decimal result;
            return decimal.TryParse(entity.ToString(), out result);
        }

        public static bool IsDate(object entity)
        {
            if (entity == null) return false;
            return IsDate(entity.ToString());
        }

        public static bool IsDateOrNullDate(object entity)
        {
            if (entity == null) return true;
            return IsDate(entity.ToString());
        }

        public static bool IsDateGreaterThanDefaultDate(object entity)
        {
            if (entity == null) return false;
            if (IsDate(entity.ToString()) == false) return false;

            DateTime testDate = Convert.ToDateTime(entity.ToString());
            long test = testDate.Ticks;
            if (test == 0) return false;

            return true;

        }

        private static bool IsDate(string date)
        {
            return DateTime.TryParse(date, out DateTime dateTime);
        }

    }
}
