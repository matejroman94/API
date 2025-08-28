using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.Exceptions {
    public static class HelperEx {

        public static StringBuilder FlattenException(Exception? exception) {
            var stringBuilder = new StringBuilder();

            while (exception != null) {
                stringBuilder.AppendLine(exception.Message);
                stringBuilder.AppendLine(exception.StackTrace);

                exception = exception.InnerException;
            }

            return stringBuilder;
        }

    }
}
