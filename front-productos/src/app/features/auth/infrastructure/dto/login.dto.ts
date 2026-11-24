export interface LoginRequestDto {
  correo: string;
  contrasena: string;
}

export interface LoginResponseDto {
  id: number;
  nombre: string;
  correo: string;
  rol: string;
  token: string;
  expiraEn: string;
}
