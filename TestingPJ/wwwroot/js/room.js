function fetchAvailableRooms() {
    const ngayDen = document.getElementById("ngayDen").value;
    const ngayDi = document.getElementById("ngayDi").value;

    fetch(`available-rooms?ngayDen=${ngayDen}&ngayDi=${ngayDi}`)
        .then(response => response.json())
        .then(data => {
            const roomList = document.getElementById("availableRooms");
            roomList.innerHTML = "";
            data.forEach(room => {
                const listItem = document.createElement("li");
                listItem.textContent = `Room Name: ${room.tenPhong}, Room Type: ${room.loaiPhong}, Price: ${room.giaCa}, Number of Beds: ${room.soGiuong}, Type of Bed: ${room.loaiGiuong}`;
                roomList.appendChild(listItem);
            });
        });
}

document.addEventListener("DOMContentLoaded", function () {
    window.fetchAvailableRooms = fetchAvailableRooms;
});
