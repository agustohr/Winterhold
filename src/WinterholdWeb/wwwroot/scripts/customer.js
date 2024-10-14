(() => {
    const URL = 'http://localhost:8010/api/v1/customer';

    let getCustomerByMembershipNumber = (memberNumber) => {
        $.ajax({
            url : `${URL}/${memberNumber}`,
            method: "GET",
            beforeSend : () => {
                document.querySelector('.loading svg').removeAttribute('hidden');
            },
            success : (response) => {
                openModal();
                bindingDataToModal(response);
            },
            complete : () => {
                document.querySelector('.loading svg').setAttribute('hidden',true);
            }
        });
    }

    let deleteCustomer = (memberNumber) => {
        $.ajax({
            url: `${URL}/${memberNumber}`,
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

    let extendExpiredDate = (memberNumber) => {
        $.ajax({
            url: `${URL}/extend/${memberNumber}`,
            method: 'PATCH',
            beforeSend : () => {
                document.querySelector('.loading svg').removeAttribute('hidden');
            },
            success: function (response) {
                alert(response);
                location.reload();
            },
            complete : () => {
                document.querySelector('.loading svg').setAttribute('hidden',true);
            }
        });
    }

    let openModal = () => {
        document.querySelector('.modal-layer').style.display = 'flex';
        document.querySelector('.popup-dialog').style.display = 'block';
    };

    let bindingDataToModal = (data) => {
        document.querySelector('#member-number').textContent = data.membershipNumber;
        document.querySelector('#fullname').textContent = data.firstName + " " + data.lastName;
        document.querySelector('#birthdate').textContent = data.birthDate;
        document.querySelector('#gender').textContent = data.gender;
        document.querySelector('#phone').textContent = data.phone;
        document.querySelector('#address').textContent = data.address;
    };

    //trigger membernumber / delete / extend
    document.querySelector('#data-customers').addEventListener('click', (event) => {
        let memberNumber = event.target.getAttribute('id');
        let className = event.target.getAttribute('class');
        if(className == 'membership-number'){
            memberNumber = event.target.textContent;
            getCustomerByMembershipNumber(memberNumber);
        }else if(className == 'black-button delete-button'){
            let confirmDelete = confirm("Are you sure you want to delete this customer?");
            if(confirmDelete){
                deleteCustomer(memberNumber);
            }
        }else if(className == 'black-button extend-button'){
            let confirmDelete = confirm("Are you sure you want to extend expired date this customer?");
            if(confirmDelete){
                extendExpiredDate(memberNumber);
            }
        }
    });

    //close modal
    document.querySelector('.close-button').addEventListener('click', () => {
        document.querySelector('.modal-layer').style.display = 'none';
        document.querySelector('.popup-dialog').style.display = 'none';
    });
})()