using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    [Serializable]
    public class ControllerExceptionSuccessAdded : Exception
    {
        public ControllerExceptionSuccessAdded(string Item) : base(String.Format("{0} added to the database successfully!", Item)) { }
    }

    [Serializable]
    public class ControllerExceptionAddedByID : Exception
    {
        public ControllerExceptionAddedByID(string Item, int ID) : base(String.Format("{0} with ID {1} cannot be added to the database!", Item, ID)) { }
    }

    [Serializable]
    public class ControllerExceptionExceptionAdded : Exception
    {
        public ControllerExceptionExceptionAdded(string Item) : base(String.Format("{0} cannot be added to the database!", Item)) { }
    }

    [Serializable]
    public class ControllerExceptionSuccessAddedByID : Exception
    {
        public ControllerExceptionSuccessAddedByID(string Item, int ID) : base(String.Format("{0} with ID {1} was added to the database successfully!", Item, ID)) { }
    }

    [Serializable]
    public class ControllerExceptionSuccessUpdated : Exception
    {
        public ControllerExceptionSuccessUpdated(string Item) : base(String.Format("{0} was added to the database successfully!", Item)) { }
    }
    [Serializable]
    public class ControllerExceptionSuccessUpdatedByID : Exception
    {
        public ControllerExceptionSuccessUpdatedByID(string Item, int ID) : base(String.Format("{0} with ID {1} was updated successfully!", Item, ID)) { }
    }

    [Serializable]
    public class ControllerExceptionExceptionUpdatedByID : Exception
    {
        public ControllerExceptionExceptionUpdatedByID(string Item, int ID) : base(String.Format("{0} with ID {1} cannot be updated!", Item, ID)) { }
    }

    [Serializable]
    public class ControllerExceptionExceptionUpdated : Exception
    {
        public ControllerExceptionExceptionUpdated(string Item) : base(String.Format("{0} cannot be updated!", Item)) { }
    }

    [Serializable]
    public class ControllerExceptionNotFoundAny : Exception
    {
        public ControllerExceptionNotFoundAny(string Item) : base(String.Format("{0} are empty!", Item)) { }
    }

    [Serializable]
    public class ControllerExceptionFoundByIdSuccess : Exception
    {
        public ControllerExceptionFoundByIdSuccess(string Item, int ID) : base(String.Format("{0} with ID {1} was found successfully!", Item, ID)) { }
    }

    [Serializable]
    public class ControllerExceptionFoundBySuccess : Exception
    {
        public ControllerExceptionFoundBySuccess(string Item, string By) : base(String.Format("{0}: {1} was found successfully!", Item, By)) { }
    }

    [Serializable]
    public class ControllerExceptionNotFoundById : Exception
    {
        public ControllerExceptionNotFoundById(string Item, int ID) : base(String.Format("{0} with ID {1} cannot be found!", Item, ID)) { }
    }

    [Serializable]
    public class ControllerExceptionNotFoundBy : Exception
    {
        public ControllerExceptionNotFoundBy(string Item, string By) : base(String.Format("{0}: {1} cannot be found!", Item, By)) { }
    }

    [Serializable]
    public class ControllerExceptionGetAllSuccess : Exception
    {
        public ControllerExceptionGetAllSuccess(string Items) : base(String.Format("All {0} have been received successfully!", Items)) { }
    }
    [Serializable]
    public class ControllerSuccessDeleteSuccessByID : Exception
    {
        public ControllerSuccessDeleteSuccessByID(string Item, int ID) : base(String.Format("{0} with ID {1} was deleted successfully!", Item, ID)) { }
    }

    [Serializable]
    public class ControllerExceptionDeleteByID : Exception
    {
        public ControllerExceptionDeleteByID(string Item, int ID) : base(String.Format("{0} with ID {1} cannot be deleted!", Item, ID)) { }
    }
    [Serializable]
    public class ControllerExceptionEventAsBytes : Exception
    {
        public ControllerExceptionEventAsBytes(string Item) : base(String.Format("{0}", Item)) { }
    }
}
