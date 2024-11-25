import styled from "styled-components";

export const PageWrapper = styled.div`
  height: 100vh;
  .row-1 {
    height: 100%;
  }
  .box-left {
    position: relative;
  }
  .bg-1 {
    max-width: 100%;
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    object-fit: cover;
  }
  .bg-2 {
    position: absolute;
    bottom: 0;
    left: 0;
    width: 100%;
    max-width: 100%;
    z-index: 2;
    object-fit: cover;
  }
  .content-login {
    border: 0.5px solid #878787;
    border-radius: 10px;
    margin: 10px;
    padding: 22px;
  }
  .title-login {
    justify-content: center;
    align-items: center;
    width: 100%;
    display: flex;
  }
`
