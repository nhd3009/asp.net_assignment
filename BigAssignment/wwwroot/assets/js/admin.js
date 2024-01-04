$(document).ready(function() {
    CKEDITOR.replace("editor");
});

$("#checkbox-product-heading").change(function() {
    let checkboxProducts = document.querySelectorAll(".checkbox-product");
    if (this.checked) {
        checkboxProducts.forEach(function(item) {
            item.checked = true;
        });
    } else {
        checkboxProducts.forEach(function(item) {
            item.checked = false;
        });
    }
});

$("#checkbox-coupon-heading").change(function() {
    let checkboxCoupons = document.querySelectorAll(".checkbox-coupon");
    if (this.checked) {
        checkboxCoupons.forEach(function(item) {
            item.checked = true;
        });
    } else {
        checkboxCoupons.forEach(function(item) {
            item.checked = false;
        });
    }
});

$("#checkbox-classify-heading").change(function() {
    let checkboxClassifies = document.querySelectorAll(".checkbox-classify");
    if (this.checked) {
        checkboxClassifies.forEach(function(item) {
            item.checked = true;
        });
    } else {
        checkboxClassifies.forEach(function(item) {
            item.checked = false;
        });
    }
});

$("#checkbox-review-heading").change(function() {
    let checkboxReviews = document.querySelectorAll(".checkbox-review");
    if (this.checked) {
        checkboxReviews.forEach(function(item) {
            item.checked = true;
        });
    } else {
        checkboxReviews.forEach(function(item) {
            item.checked = false;
        });
    }
});

$("#checkbox-order-heading").change(function() {
    let checkboxOrders = document.querySelectorAll(".checkbox-order");
    if (this.checked) {
        checkboxOrders.forEach(function(item) {
            item.checked = true;
        });
    } else {
        checkboxOrders.forEach(function(item) {
            item.checked = false;
        });
    }
});

$("#checkbox-user-heading").change(function() {
    let checkboxUsers = document.querySelectorAll(".checkbox-user");
    if (this.checked) {
        checkboxUsers.forEach(function(item) {
            item.checked = true;
        });
    } else {
        checkboxUsers.forEach(function(item) {
            item.checked = false;
        });
    }
});

$(document).ready(function() {
    $('#product-table').DataTable({
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
    $('#coupon-table').DataTable({
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
    $('#classify-table').DataTable({
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
    $('#review-table').DataTable({
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
    $('#order-table').DataTable({
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
    $('#user-table').DataTable({
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
    $('#revenue-table').DataTable({
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
        },
    });
});