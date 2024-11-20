import axios, { HttpStatusCode } from "axios";

const authCheckPath = "/api/account/whoami";
const authLoginPath = "/api/account/login";
const authLogoutPath = "/api/account/logout";

export const fetchAuthStatusFn = async (): Promise<string | null> => {
  try {
    const response = await axios.get(authCheckPath, { withCredentials: true });
    return response.data;
  } catch (error) {
    return null;
  }
};

export const loginUserFn = async (credentials: {
  email: string;
  password: string;
}): Promise<string | null> => {
  const response = await axios.post(authLoginPath, credentials, {
    withCredentials: true,
  });

  if (response.status === HttpStatusCode.Ok) {
    return fetchAuthStatusFn();
  }

  return null;
};

export const logoutUserFn = async () => {
  await axios.post(authLogoutPath, {}, { withCredentials: true });
};
