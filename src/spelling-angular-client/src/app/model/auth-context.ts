import { UserProfile } from './user-profile';
import { SimpleClaim } from './simple-claim';

export class AuthContext {
  claims: SimpleClaim[];
  userProfile: UserProfile;

  get isAdmin() {
    return !!this.claims && !!this.claims.find(c =>
      c.type === 'role' && c.value === 'Admin');
  }

  hasClaims(...claims: string[]): boolean {
    return claims.every(claim => this.claims.map(claim => claim.value).includes(claim));
  }
}