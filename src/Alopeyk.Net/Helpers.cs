namespace Alopeyk.Net
{
    public static class AlopeykHelpers
    {
        public static string JoinUrls(
            string left,
            string right
        )
        {
            if (left is null) return right;
            if (right is null) return left;

            if (left[left.Length - 1] == '/')
            {
                if (right[0] == '/')
                {
                    return left + right.Substring(1);
                }

                return left + right;
            }

            if (right[0] == '/')
            {
                return left + right;
            }

            return left + '/' + right;
        }
    }
}