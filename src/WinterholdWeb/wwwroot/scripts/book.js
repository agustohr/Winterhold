(() => {
    const URL = 'http://localhost:8010/api/v1/book';
    
    let openModal = (summary) => {
        document.querySelector('#summary-content').textContent = summary;
        document.querySelector('.modal-layer').style.display = 'flex';
        document.querySelector('.popup-dialog').style.display = 'block';
    };

    let deleteBook = (code) => {
        $.ajax({
            url: `${URL}/${code}`,
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
    };
    
    //trigger button summary & delete
    document.querySelector('#data-books').addEventListener('click', (event) => {
        let summary = event.target.getAttribute('content');
        let code = event.target.getAttribute('id');
        if(summary != null){
            openModal(summary);
        }else if(code != null){
            let confirmDelete = confirm("Are you sure you want to delete this book?");
            if(confirmDelete){
                deleteBook(code);
            }
        }
    });

    //close modal
    document.querySelector('.close-button').addEventListener('click', () => {
        document.querySelector('.modal-layer').style.display = 'none';
        document.querySelector('.popup-dialog').style.display = 'none';
    });
})()