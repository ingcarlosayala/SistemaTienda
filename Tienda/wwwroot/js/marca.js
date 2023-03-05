var datatable;

$(document).ready(() => {
    CargarTabla();
});

const CargarTabla = () => {
    datatable = $("#tblmarca").DataTable({
        "ajax": {
            "url": "/Admin/Marcas/GetAll"
        },
        "columns":[
            {"data": "nombre", "width": "50%"},
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Marcas/Editar/${data}" class="btn btn-warning btn-sm"><i class="fa-solid fa-pen-to-square"></i></a>
                                <a onclick=Delete("/Admin/Marcas/Delete/${data}") class="btn btn-danger btn-sm"><i class="fa-solid fa-trash"></i></a>
                            </div>`;
                },"width": "35%"
            }
        ]
    });
}

const Delete = url => {
    Swal.fire({
        title: 'Estas Seguro de Eliminar?',
        text: "Al elimnar no se podra revertir!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, Eliminar!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data) {
                        toastr.success(data.message);
                        datatable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    })
}