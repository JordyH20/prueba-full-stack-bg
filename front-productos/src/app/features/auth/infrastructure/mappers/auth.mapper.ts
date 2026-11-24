import { Sesion } from '../../domain/models/sesion';
import { LoginResponseDto } from '../dto/login.dto';

export function mapLoginResponse(dto: LoginResponseDto): Sesion {
  return {
    id: dto.id,
    nombre: dto.nombre,
    correo: dto.correo,
    rol: dto.rol,
    token: dto.token,
    expiraEn: dto.expiraEn
  };
}
