using MongoDB.Driver;
using MoviesApi.Core.Common;

namespace MoviesApi.Utils
{
    public static class ApiUtils
    {
        public static Result ProcessResult(UpdateResult result, string unmatchedMessage = null,
                                                       bool ignoreWhenMatchButNoChange = true, string msgWhenMatchButNoChange = null,
                                                       string updateException = null)
        {
            if (!result.IsAcknowledged)
            {
                return new Result(false, updateException ?? "Exception Occured");
            }
            if (result.MatchedCount == 0)
            {
                return new Result(false, unmatchedMessage);
            }
            if (result.MatchedCount > 0 && result.ModifiedCount == 0)
            {
                return new Result(ignoreWhenMatchButNoChange, msgWhenMatchButNoChange ?? "Couldn't Update");
            }
            return new Result(true);
        }
    }
}
