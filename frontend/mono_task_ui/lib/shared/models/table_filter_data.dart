class TableFilterData {
  final int page;
  final String sortBy;
  final String sortOrder;
  final String searchValue;

  TableFilterData({
    this.page = 1,
    this.sortBy = 'Name',
    this.sortOrder = 'asc',
    this.searchValue = '',
  });

  Map<String, dynamic> toQueryParams() {
    return {
      'page': page,
      'sortBy': sortBy,
      'sortOrder': sortOrder,
      'searchValue': searchValue,
    };
  }
}
