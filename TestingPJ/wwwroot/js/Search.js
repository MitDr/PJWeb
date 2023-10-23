function searchCustomer() {
    const maKhachHang = document.getElementById("maKhachHang").value;
    const sdt = document.getElementById("sdt").value;

    fetch(`search-customer?maKhachHang=${maKhachHang}&sdt=${sdt}`)
        .then(response => response.json())
        .then(data => {
            const custlist = document.getElementById("customerslist");
            custlist.innerHTML = "";
            data.forEach(customers => {
                const listItem = document.createElement("li");
                listItem.textContent = `Mã khách hàng: ${customers.maKhachHang}, Họ: ${customers.ho}, Đệm:${customers.dem}, Tên:${customers.ten}`;
                custlist.appendChild(listItem);
            });
        });
}
