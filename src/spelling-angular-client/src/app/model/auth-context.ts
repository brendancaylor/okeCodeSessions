


export class AuthContext {

  claims: Array<string>;

  hasClaims(...claims: string[]): boolean {
    return claims.every(claim => this.claims.includes(claim));
  }

}
