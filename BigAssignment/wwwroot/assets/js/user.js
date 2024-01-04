$(document).ready(function() {
    $('#user-info-table').DataTable({
        "language": {
            "decimal":        "",
            "emptyTable":     "Không có dữ liệu",
            "info":           "Hiển thị từ _START_ đến _END_ của _TOTAL_ kết quả",
            "infoEmpty":      "Hiển thị 0 kết quả",
            "infoFiltered":   "(đã lọc từ _MAX_ kết quả)",
            "lengthMenu":     "Hiển thị _MENU_ kết quả",
            "search":         "Tìm kiếm:",
            "zeroRecords":    "Không tìm thấy kết quả",
            "paginate": {
                "next":       "Tiếp theo",
                "previous":   "Trước đó"
            }
        }
    });
});