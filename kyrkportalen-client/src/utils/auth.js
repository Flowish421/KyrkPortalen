import { jwtDecode } from "jwt-decode";


export function getUserRole() {
  const token = localStorage.getItem("token");
  if (!token || token === "undefined") return null;

  try {
    const decoded = jwtDecode(token);
    return decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
  } catch {
    return null;
  }
}

export function isAdmin() {
  return getUserRole() === "Admin";
}
