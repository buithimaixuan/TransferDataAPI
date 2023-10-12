const DEFAULT_URL_API = 'https://localhost:7297/api/';
export const URL_ENDPOINT = {
  FACILITY: {
    GET_ALL_FACILITY: DEFAULT_URL_API + 'Facility/get-all-facility',
    GET_FACILITY_BY_ID: DEFAULT_URL_API + 'Facility/get-facility-by-id/',
    ADD_FACILITY: DEFAULT_URL_API + 'Facility/add-facility',
    UPDATE_FACILITY_BY_ID: DEFAULT_URL_API + 'Facility/update-facility-by-id/',
    DELETE_FACILITY_BY_ID: DEFAULT_URL_API + 'Facility/delete-facility-by-id/',
  },
  RESIDENT: {
    GET_ALL_RESIDENT: DEFAULT_URL_API + 'Resident/get-all-resident',
    GET_RESIDENT_BY_ID: DEFAULT_URL_API + 'Resident/get-resident-by-id/',
    ADD_RESIDENT: DEFAULT_URL_API + 'Resident/add-resident',
    UPDATE_RESIDENT_BY_ID: DEFAULT_URL_API + 'Resident/update-resident-by-id/',
    DELETE_RESIDENT_BY_ID: DEFAULT_URL_API + 'Resident/delete-resident-by-id/',
  },
  PROGRESS_NOTE: {
    GET_ALL_PROGRESS_NOTE:
      DEFAULT_URL_API + 'ProgressNote/get-all-progressNote',
    GET_PROGRESS_NOTE_BY_ID:
      DEFAULT_URL_API + 'ProgressNote/get-progressNote-by-id/',
    ADD_PROGRESS_NOTE: DEFAULT_URL_API + 'ProgressNote/add-progressNote',
    UPDATE_PROGRESS_NOTE_BY_ID:
      DEFAULT_URL_API + 'ProgressNote/update-progressNote-by-id/',
    DELETE_PROGRESS_NOTE_BY_ID:
      DEFAULT_URL_API + 'ProgressNote/delete-progressNote-by-id/',
  },
};
