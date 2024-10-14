(() => {
    const URL = 'http://localhost:8010/api/v1/category';
    var CATEGORY_NAME;

    let getCategoryByName = () => {
        $.ajax({
            url : `${URL}/${CATEGORY_NAME}`,
            method: "GET",
            beforeSend : () => {
                console.log("Getting Data ...");
                document.querySelector('.loading svg').removeAttribute('hidden');
            },
            success : (response) => {
                document.querySelector('input[name=categoryName]').setAttribute('disabled', true);
                openModalUpsert();
                bindingDataToModal(response);
            },
            complete : () => {
                console.log("Finish ...");
                document.querySelector('.loading svg').setAttribute('hidden','');
            }
        });
    }

    let insertCategory = (data) => {
        $.ajax({
            url : URL,
            method: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data),
            beforeSend : () => {
                document.querySelector('.loading svg').removeAttribute('hidden');
            },
            success : (response) => {
                alert(`${response.name} Category Added Successfully`);
                location.reload();
            },
            complete : () => {
                document.querySelector('.loading svg').setAttribute('hidden',true);
            },
            error : (response) => {
                alert(response.responseText);
            }
        });
    }

    let updateCategory = (data) => {
        $.ajax({
            url : `${URL}/${CATEGORY_NAME}`,
            method: "PUT",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data),
            beforeSend : () => {
                document.querySelector('.loading svg').removeAttribute('hidden');
            },
            success : (response) => {
                alert(`${response.name} Category Updated Successfully`);
                location.reload();
            },
            complete : () => {
                document.querySelector('.loading svg').setAttribute('hidden',true);
            },
            error : (response) => {
                alert(response.responseText);
            }
        });
    }

    let deleteCategory = () => {
        $.ajax({
            url: `${URL}/${CATEGORY_NAME}`,
            method: 'DELETE',
            beforeSend : () => {
                document.querySelector('.loading svg').removeAttribute('hidden');
            },
            success: function (response) {
                alert(response);
                location.reload();
            },
            complete : () => {
                document.querySelector('.loading svg').setAttribute('hidden',true);
            },
            error : (response) => {
                alert(response.responseText);
            }
        });
    }

    let openModalUpsert = () => {
        document.querySelector('input[name=categoryName]').value = '';
        document.querySelector('input[name=floor]').value = 0;
        document.querySelector('input[name=isle]').value = '';
        document.querySelector('input[name=bay]').value = '';
        document.querySelector('.modal-layer').style.display = 'flex';
        document.querySelector('.popup-dialog').style.display = 'block';
    };

    let bindingDataToModal = (data) => {
        document.querySelector('input[name=categoryName]').value = data.name;
        document.querySelector('input[name=floor]').value = data.floor;
        document.querySelector('input[name=isle]').value = data.isle;
        document.querySelector('input[name=bay]').value = data.bay;
    };

    //trigger button add
    document.querySelector('.add-button').addEventListener('click', () => {
        openModalUpsert();
    });

    //trigger button edit delete
    document.querySelector('#data-category').addEventListener('click', (event) => {
        CATEGORY_NAME = event.target.getAttribute('id');
        if(event.target.getAttribute('class') == 'black-button edit-button'){
            getCategoryByName();
        }else if(event.target.getAttribute('class') == 'black-button delete-button'){
            let confirmDelete = confirm("Are you sure you want to delete this category?");
            if(confirmDelete){
                deleteCategory();
            }
        }

    });

    //close modal
    document.querySelector('.close-button').addEventListener('click', () => {
        document.querySelector('.modal-layer').style.display = 'none';
        document.querySelector('.popup-dialog').style.display = 'none';
        document.querySelector('input[name=categoryName]').removeAttribute('disabled');
    });

    //trigger button submit
    document.querySelector('#upsert-category').addEventListener('submit', (event) => {
        event.preventDefault();
        let name = document.querySelector('input[name=categoryName]').value;
        let flor = document.querySelector('input[name=floor]').value;
        let isle = document.querySelector('input[name=isle]').value;
        let bay = document.querySelector('input[name=bay]').value;
        let data = {
            Name: name,
            Floor: flor,
            Isle: isle,
            Bay: bay
        }
        if(document.querySelector('input[name=categoryName]').disabled){
            updateCategory(data);
        }else{
            insertCategory(data);
        }
    });
})()