namespace Common
{
    public static class CopyClasses
    {
        public static void Copy<U,V>(U objIn, ref V objOut)
        {
            if (objIn is null) return;
            if (objOut is null) return;

            var propInfo = objIn.GetType().GetProperties();
            foreach (var en in propInfo)
            {
                var type = en.Name;
                objOut.GetType().GetProperty(en.Name)?.SetValue(objOut, en.GetValue(objIn, null), null);
            }
        }
    }
}