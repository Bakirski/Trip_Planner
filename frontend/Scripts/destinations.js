const tripId = sessionStorage.getItem("tripId");
const bearerToken = localStorage.getItem("bearerToken");

window.onload = function () {
  getTrip(tripId);
};

async function getTrip(id) {
  try {
    const response = await fetch(`http://localhost:5063/api/trips/${id}`, {
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${bearerToken}`,
      },
    });
    const response2 = await fetch(
      `http://localhost:5063/api/trips/${id}/destinations`,
      {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${bearerToken}`,
        },
      }
    );

    const tripData = await response.json();
    const destinationData = await response2.json();

    console.log(tripData);
    console.log(destinationData);
    const data = [tripData, destinationData.length ? destinationData : []];
    displayData(data);
  } catch (error) {
    console.log("Error fetching data: ", error);
  }
}

function displayData(data) {
  const destinationsContainer = document.getElementById(
    "destinationsContainer"
  );

  const tripTitle = document.getElementById("tripTitle");
  tripTitle.textContent = data[0].tripName;

  if (data[1].length > 0) {
    data[1].forEach((item) => {
      const dataItem = document.createElement("p");
      dataItem.textContent = item.destinationName;
      destinationsContainer.appendChild(dataItem);
    });
  } else null;
}
