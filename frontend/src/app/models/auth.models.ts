export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  nombre: string;
  email: string;
  password: string;
  equipo: string;
}

export interface UserMe {
  nombre: string;
  email: string;
  equipo: string;
}

export interface AuthResponse {
  token: string;
}
