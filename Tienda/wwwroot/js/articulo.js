var datatable;

$(document).ready(() => {
    CargarTabla();
});

const CargarTabla = () => {
    datatable = $("#tblarticulo").DataTable({
        "ajax": {
            "url": "/Admin/Articulos/GetAll"
        },
        "columns": [
            {"data": "codigo", "width": "15%"},
            {"data": "nombre", "width": "15%"},
            {"data": "categoria.nombre", "width": "15%"},
            {"data": "marca.nombre", "width": "15%"},
            {"data": "precio", "width": "15%"},
            { "data": "fechaCreacion", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Articulos/Editar/${data}" class="btn btn-warning btn-sm"><i class="fa-solid fa-pen-to-square"></i></a>
                                <a onclick=Delete("/Admin/Articulos/Delete/${data}") class="btn btn-danger btn-sm"><i class="fa-solid fa-trash"></i></a>
                            </div>`;
                },"width": "15%"
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