(() => {
    const URL = 'http://localhost:8010/api/v1/loan';
    var ID = 0;

    let getSelectList = async (code) => {
        await $.ajax({
            url : `${URL}/selectlist/${code}`,
            method: "GET",
            success : (response) => {
                bindingSelectListToModalUpsert(response);
            },
        });
    };

    let getLoanById = async () => {
        let response = await $.ajax({
            url : `${URL}/${ID}`,
            method: "GET"
        });
        await getSelectList(response.bookCode);
        bindingdataToModalUpsert(response);
    }

    let getDetailLoan = () => {
        $.ajax({
            url : `${URL}/detail/${ID}`,
            method: "GET",
            beforeSend : () => {
                document.querySelector('.loading svg').removeAttribute('hidden');
            },
            success : (response) => {
                bindingdataToModalDetail(response);
                openModalDetail();
            },
            complete : () => {
                document.querySelector('.loading svg').setAttribute('hidden',true);
            }
        });
    }

    let insertLoan = (data) => {
        $.ajax({
            url : URL,
            method: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data),
            beforeSend : () => {
                document.querySelector('.loading svg').removeAttribute('hidden');
            },
            success : () => {
                alert(`Loan Was Added Successfully`);
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

    let updateLoan = (data) => {
        $.ajax({
            url : `${URL}/${ID}`,
            method: "PUT",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data),
            beforeSend : () => {
                document.querySelector('.loading svg').removeAttribute('hidden');
            },
            success : () => {
                alert(`Loan Was Updated Successfully`);
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

    let setReturn = () => {
        $.ajax({
            url : `${URL}/return/${ID}`,
            method: "PATCH",
            beforeSend : () => {
                document.querySelector('.loading svg').removeAttribute('hidden');
            },
            success : (response) => {
                alert(response);
                location.reload();
            },
            complete : () => {
                document.querySelector('.loading svg').setAttribute('hidden',true);
            }
        });
    }

    let bindingSelectListToModalUpsert = (data) => {
        let selectCustomer = document.querySelector('select[name=customer]');
        selectCustomer.innerHTML = '<option value="">Select Customer</option>';
        data.selectListCustomer.forEach(customer => {
            selectCustomer.innerHTML += `<option value="${customer.value}">${customer.text}</option>`;
        });
        let selectbook = document.querySelector('select[name=book]');
        selectbook.innerHTML = '<option value="">Select Book</option>';
        data.selectListBook.forEach(book => {
            selectbook.innerHTML += `<option value="${book.value}">${book.text}</option>`;
        });
    };

    let bindingdataToModalUpsert = (data) => {

        document.querySelector('select[name=customer]').value = data.customerNumber;
        document.querySelector('select[name=book]').value = data.bookCode;
        document.querySelector('input[name=loan-date]').value = formatDate(data.loanDate);
        document.querySelector('textarea[name=note]').value = data.note;
    };
    let bindingdataToModalDetail = (data) => {
        document.querySelector('#detail-title').textContent = data.title;
        document.querySelector('#detail-category').textContent = data.category;
        document.querySelector('#detail-author').textContent = data.author;
        document.querySelector('#detail-floor').textContent = data.floor;
        document.querySelector('#detail-isle').textContent = data.isle;
        document.querySelector('#detail-bay').textContent = data.bay;
        document.querySelector('#detail-membernumber').textContent = data.membershipNumber;
        document.querySelector('#detail-fullname').textContent = data.fullName;
        document.querySelector('#detail-phone').textContent = data.phone;
        document.querySelector('#detail-expiredate').textContent = data.membershipExpiredDate;
    };

    let openModalUpsert = () => {
        document.querySelector('.modal-upsert').style.display = 'flex';
        document.querySelector('.popup-upsert').style.display = 'block';
    };
    let openModalDetail = () => {
        document.querySelector('.modal-detail').style.display = 'flex';
        document.querySelector('.popup-detail').style.display = 'block';
    };

    //trigger button add
    document.querySelector('.add-button').addEventListener('click', async () => {
        document.querySelector('.loading svg').removeAttribute('hidden');
        await getSelectList();
        document.querySelector('.loading svg').setAttribute('hidden', true);
        openModalUpsert();
    });

    //trigger button detail edit return
    document.querySelector('#data-loans').addEventListener('click', async (event) => {
        ID = event.target.getAttribute('id');
        let clasName = event.target.getAttribute('class');
        if(clasName == 'black-button detail-button'){
            getDetailLoan();
            
        }else if(clasName == 'black-button edit-button'){
            document.querySelector('.loading svg').removeAttribute('hidden');
            await getLoanById();
            document.querySelector('.loading svg').setAttribute('hidden', '');
            openModalUpsert();
        }else if(clasName == 'black-button return-button'){
            let confirmDelete = confirm("Are you sure you want to return this loan?");
            if(confirmDelete){
                setReturn();
            }
        }
    });

    //trigger button submit
    document.querySelector('#upsert-loan').addEventListener('submit', (event) => {
        event.preventDefault();
        let customer = document.querySelector('select[name=customer]').value;
        let book = document.querySelector('select[name=book]').value;
        let loandate = document.querySelector('input[name=loan-date]').value;
        let note = document.querySelector('textarea[name=note]').value;
        let data = {
            Id: 0,
            CustomerNumber: customer,
            BookCode: book,
            LoanDate: loandate,
            Note: note
        }
        data.LoanDate = data.LoanDate == '' ? new Date('0001-01-01') : data.LoanDate;
        console.log(data);
        if(ID == 0){
            insertLoan(data);
        }else{
            data.Id = ID;
            updateLoan(data);
        }
    });

    //close modal upsert
    document.querySelector('.close-button-upsert').addEventListener('click', () => {
        document.querySelector('.modal-upsert').style.display = 'none';
        document.querySelector('.popup-upsert').style.display = 'none';
        document.querySelector('select[name=customer]').value = '';
        document.querySelector('select[name=book]').value = '';
        document.querySelector('input[name=loan-date]').value = '';
        document.querySelector('textarea[name=note]').value = '';
        ID = 0;
    });

    //close modal detail
    document.querySelector('.close-button-detail').addEventListener('click', () => {
        document.querySelector('.modal-detail').style.display = 'none';
        document.querySelector('.popup-detail').style.display = 'none';
    });

    let formatDate = (date) => {
        var d = new Date(date),
            month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = d.getFullYear();
    
        if (month.length < 2) 
            month = '0' + month;
        if (day.length < 2) 
            day = '0' + day;
    
        return [year, month, day].join('-');
    }
})()