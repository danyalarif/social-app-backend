using System;

namespace social_app_backend.Utils;

public class PaginationHelper
{
    public static (int, int?) PreparePaginationData(int? page, int? limit) {
        int actualPage = (page ?? 1) <= 0 ? 1 : page ?? 1;
        if (limit.HasValue && limit.Value != 0) {
            limit = Math.Abs(limit.Value);
        }
        return (actualPage, limit);
    }
}
