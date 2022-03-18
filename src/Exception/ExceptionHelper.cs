using System;

namespace Aisoftware.Tracker.Admin
{
    public static class ExceptionHelper
    {
        public static Exception InnerException(Exception e)
        {
            if (e?.InnerException != null)
            {
                InnerException(e.InnerException);
            }

            return e;
        }
    }
}