// Write your Javascript code.
(function ($) {
  $("#PageSize").change(function () {
    document.location = "/Home/List" +
      "?pageIndex=" + $(this).data("pg-index") +
      "&pageSize=" + $(this).val() +
      "&criteria=" + $(this).data("sort-criteria") +
      "&sortDirection=" + $(this).data("sort-dir");
  });

  $("#SortCriteria").change(function () {
    document.location = "/Home/List" +
      "?pageIndex=" + $(this).data("pg-index") +
      "&pageSize=" + $(this).data("pg-size") +
      "&criteria=" + $(this).val() +
      "&sortDirection=" + $(this).data("sort-dir");
  });

  $("#SortDirection").change(function () {
    document.location = "/Home/List" +
      "?pageIndex=" + $(this).data("pg-index") +
      "&pageSize=" + $(this).data("pg-size") +
      "&criteria=" + $(this).data("sort-criteria") +
      "&sortDirection=" + $(this).val();
  });
}(jQuery));