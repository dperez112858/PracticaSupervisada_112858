import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BajaProveedorComponent } from './baja-proveedor.component';

describe('BajaProveedorComponent', () => {
  let component: BajaProveedorComponent;
  let fixture: ComponentFixture<BajaProveedorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BajaProveedorComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BajaProveedorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
