namespace leavetracker
{
    public class EnumStringMap
    {
        public static Status ToEnum(string status)
        {
            if (string.Equals(status.ToLower(), Constants.Pending.ToLower()))
            {
                return Status.Pending;
            }

            if (string.Equals(status.ToLower(), Constants.Approved.ToLower()))
            {
                return Status.Approved;
            }

            if (string.Equals(status.ToLower(), Constants.Rejected.ToLower()))
            {
                return Status.Rejected;
            }

            return Status.Other;
        }

        public static string ToString(Status status)
        {
            switch (status)
            {
                case Status.Pending:
                    return Constants.Pending;

                case Status.Approved:
                    return Constants.Approved;

                case Status.Rejected:
                    return Constants.Rejected;

                default:
                    return Constants.Other;

            }
        }
    }
}