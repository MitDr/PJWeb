function fetchAvailableRooms() {
    const ngayDen = document.getElementById("ngayDen").value;
    const ngayDi = document.getElementById("ngayDi").value;

    fetch(`available-rooms2?ngayDen=${ngayDen}&ngayDi=${ngayDi}`)
        .then(response => response.json())
        .then(data => {
            const roomList = document.getElementById("availableRooms");
            roomList.innerHTML = "";
            data.forEach(room => {
                const listItem = document.createElement("li");
                listItem.innerHTML = `Room Name: ${room.tenPhong}, Room Type: ${room.loaiPhong}, Price: ${room.giaCa}, Number of Beds: ${room.soGiuong}, Type of Bed: ${room.loaiGiuong} <button onclick="bookRoom('${room.tenPhong}')">Book</button>`;
                roomList.appendChild(listItem);
            });
        });
}
function bookRoom(roomName) {
    const checkin = document.getElementById("ngayDen").value;
    const checkout = document.getElementById("ngayDi").value;
    const numberOfGuests = document.getElementById("soNguoi").value;  // Assume you have an input for this
    const specialRequests = document.getElementById("yeuCau").value;  // Assume you have an input for this

    const bookingData = {
        tenPhong: roomName,
        ngayDen: checkin,
        ngayDi: checkout,
        soNguoi: numberOfGuests,
        yeuCau: specialRequests
    };

    fetch('book-room', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(bookingData)
    })
        .then(response => response.json())
        .then(data => {
            if (data.message) {
                alert(data.message);  // Show a success message
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Booking failed');  // Show an error message
        });
}


document.addEventListener("DOMContentLoaded", function () {
    window.fetchAvailableRooms = fetchAvailableRooms;
});
