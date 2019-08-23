var dataTable;
$(document).ready(()=> {
    loadDataTable();
});

loadDataTable = () => {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/admin/frequency/GetAll",
            "type": "GET",
            "datatype" : "json"
        },
        "columns": [
            { "data": "name", "width": "50%" },
            { "data": "frequencyCount", "width": "20%" },
            {
                "data": "id",
                "render": (data) => {
                    return `<div class="text-center">
                                <a href="/Admin/frequency/Upsert/${data}" class='btn btn-success text-white' style='cursor:pointer;width:100px'>
                                    <i class='far fa-edit'></i>&nbsp;Edit
                                </a>
                                 &nbsp;
                                 <a onclick=Delete("/Admin/frequency/Delete/${data}") class='btn btn-danger text-white' style='cursor:pointer;width:100px'>
                                    <i class='far fa-trash-alt'></i>&nbsp;Delete
                                </a>
                            </div>
                    `
                },"width":"30%"
            }
        ],
        "language": {
            "emptyTable" : "No records found"
        },
        "width":"100%"
    });
}

