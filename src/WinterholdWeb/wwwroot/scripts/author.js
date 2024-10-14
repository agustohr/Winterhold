(() => {
    const URL = 'http://localhost:8010/api/v1/category';

    let deleteAuthor = (id) => {
        $.ajax({
            url: `${URL}/${id}`,
            method: 'DELETE',
            beforeSend : () => {
                document.querySelector('.loading svg').removeAttribute('hidden');
            },
            success: function () {
                alert(`Author Was Deleted Successfully`);
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

    //trigger button delete
    document.querySelector('.data-author').addEventListener('click', (event) => {
        let id = event.target.getAttribute('id');
        if(id != null){
            deleteAuthor(id);
        }
    });
})()