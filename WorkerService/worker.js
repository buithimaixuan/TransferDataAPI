import axios from "axios";
process.env.NODE_TLS_REJECT_UNAUTHORIZED = "0";

// API endpoint
const apiUrlServerA = "https://localhost:7296/api";
const apiUrlServerB = "https://localhost:7169/api";

// Schema
const Facility = "Facility";
const Resident = "Resident";
const ProgressNote = "ProgressNote";

const syncDataFacility = async () => {
  try {
    const resA = await axios.get(
      `${apiUrlServerA}/${Facility}/get-all-facility`
    );
    const dataA = resA.data;

    // //delete old data of ServerB
    await axios.delete(`${apiUrlServerB}/${Facility}/delete-all-facility`);
    // //Send dataA to API B
    const resB = await axios.post(
      `${apiUrlServerB}/${Facility}/add-list-facility`,
      dataA
    );

    console.log("Data Facility synced successfully! ", resB.data);
  } catch (error) {
    console.error("Error syncing data Facility:", error.message);
  }
};

const syncDataResident = async () => {
  try {
    const resA = await axios.get(
      `${apiUrlServerA}/${Resident}/get-all-resident`
    );
    const dataA = resA.data;

    //delete old data of ServerB
    await axios.delete(`${apiUrlServerB}/${Resident}/delete-all-resident`);
    //Send dataA to API B
    const resB = await axios.post(
      `${apiUrlServerB}/${Resident}/add-list-resident`,
      dataA
    );

    console.log("Data Resident synced successfully! ", resB.data);
  } catch (error) {
    console.error("Error syncing data Resident:", error.message);
  }
};

const syncDataProgressNote = async () => {
  try {
    const resA = await axios.get(
      `${apiUrlServerA}/${ProgressNote}/get-all-progressNote`
    );
    const dataA = resA.data;

    //delete old data of ServerB
    await axios.delete(
      `${apiUrlServerB}/${ProgressNote}/delete-all-progressNote`
    );
    //Send dataA to API B
    const resB = await axios.post(
      `${apiUrlServerB}/${ProgressNote}/add-list-progressNote`,
      dataA
    );

    console.log("Data ProgressNote synced successfully! ", resB.data);
  } catch (error) {
    console.error("Error syncing data ProgressNote:", error.message);
  }
};

// Function to call the three sync functions sequentially
const syncAllData = async () => {
  await syncDataFacility();
  await syncDataResident();
  await syncDataProgressNote();
};

// Set up a timer to run the syncAllData function every 30 seconds
setInterval(syncAllData, 5000);

// Start the initial sync immediately
syncAllData();
