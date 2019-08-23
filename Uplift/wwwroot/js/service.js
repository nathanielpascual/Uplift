var dataTable;
$(document).ready(()=> {
    loadDataTable();
});

loadDataTable = () => {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/admin/service/GetAll",
            "type": "GET",
            "datatype" : "json"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "category.name", "width": "20%" },
            { "data": "price", "width": "20%" },
            { "data": "frequency.frequencyCount", "width": "20%" },  
            {
                "data": "id",
                "render": (data) => {
                    return `<div class="text-center">
                                <a href="/Admin/service/Upsert/${data}" class='btn btn-success text-white' style='cursor:pointer;width:100px'>
                                    <i class='far fa-edit'></i>&nbsp;Edit
                                </a>
                                 &nbsp;
                                 <a onclick=Delete("/Admin/service/Delete/${data}") class='btn btn-danger text-white' style='cursor:pointer;width:100px'>
                                    <i class='far fa-trash-alt'></i>&nbsp;Delete
                                </a>
                            </div>
                    `
                },"width":"20%"
            }
        ],
        "language": {
            "emptyTable" : "No records found"
        },
        "width":"100%"
    });
}

