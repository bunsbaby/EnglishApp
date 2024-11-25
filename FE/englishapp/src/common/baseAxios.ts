import axios from "axios";
axios.defaults.baseURL = `${process.env.REACT_APP_URL_BASE_API}`;
axios.defaults.headers.common = {"Access-Control-Allow-Origin": "*"};
axios.defaults.headers.common = {"Access-Control-Allow-Credentials": "true"};
axios.defaults.headers.common = {"Access-Control-Allow-Methods": "POST,GET,OPTIONS,PUT,DELETE"};
axios.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error?.response?.status === 401) {
      return window.location.href = "/login";
    }
    return Promise.reject(error);
  }
);
export default axios;