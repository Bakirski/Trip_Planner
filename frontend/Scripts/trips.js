var bearerToken = localStorage.getItem("bearerToken");

window.onload = function () {
  console.log(window.location.pathname);
  getTrips();
};

async function getTrips() {
  try {
    const response = await fetch("http://localhost:5063/api/trips", {
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${bearerToken}`,
      },
    });

    const trips = await response.json();
    console.log(trips);
    displayData(trips);
  } catch (error) {
    console.error("Error fetching trips: ", error);
  }
}

function displayData(data) {
  const dataContainer = document.getElementById("dataContainer");

  dataContainer.innerHTML = "";

  data.forEach((item) => {
    const dataItem = document.createElement("div");
    dataItem.classList.add("data-item");
    dataItem.textContent = `
      ID: ${item.id},
      Name: ${item.tripName}, 
      Destination: ${item.destination}, 
      Description: ${item.description}, 
      Start Date: ${item.startDate}, 
      End Date: ${item.endDate}`;
    dataContainer.appendChild(dataItem);
  });
}
