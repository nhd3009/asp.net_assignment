$(document).ready(function () {
    $('#cate1').DataTable({
        columnDefs: [{
            "targets": 'no-sort', // add class to html will no sort
            "orderable": false,
        }],
        "order": [

            [3, "asc"], // display order
        ],
        "language": {
            "sProcessing": "Đang xử lý...",
            "sLengthMenu": "Xem _MENU_  mục",
            "sZeroRecords": "Không tìm thấy kết quả phù hợp",
            "sInfo": "Đang xem _START_ đến _END_ trong tổng số _TOTAL_ mục",
            "sInfoEmpty": "Đang xem 0 đến 0 trong tổng số 0 mục",
            "sInfoFiltered": "(được lọc từ _MAX_ mục)",
            "sInfoPostFix": "",
            "sSearch": "Tìm kiếm:",
            "sUrl": ""

        }
    });
    $('#product').DataTable({
        columnDefs: [{
            "targets": 'no-sort',
            "orderable": false,
        }],
        "order": [
        ],
    });


});

//tooltip
$(function () {
    $('[data-toggle="tooltip"]').tooltip()
});

$(document).ready(function () {
    $("*").removeClass('d-flex');
    $(".replyde").click(function () {
        $("#delay").addClass('d-flex');
    })
})
/*
"language": {
      "sProcessing":   "Đang xử lý...",
      "sLengthMenu":   "Xem _MENU_ mục",
      "sZeroRecords":  "Không tìm thấy dòng nào phù hợp",
      "sInfo":         "Đang xem _START_ đến _END_ trong tổng số _TOTAL_ mục",
      "sInfoEmpty":    "Đang xem 0 đến 0 trong tổng số 0 mục",
      "sInfoFiltered": "(được lọc từ _MAX_ mục)",
      "sInfoPostFix":  "",
      "sSearch":       "Tìm:",
      "sUrl":          "",
      "oPaginate": {
          "sFirst":    "Đầu",
          "sPrevious": "Trước",
          "sNext":     "Tiếp",
          "sLast":     "Cuối"
          }
      },
      "processing": true, // tiền xử lý trước
      "aLengthMenu": [[5, 10, 20, 50], [5, 10, 20, 50]], // danh sách số trang trên 1 lần hiển thị bảng
      "order": [[ 1, 'desc' ]] //sắp xếp giảm dần theo cột thứ 1



*/